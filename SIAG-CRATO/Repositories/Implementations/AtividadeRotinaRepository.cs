using Dapper;
using SIAG_CRATO.BLLs.Endereco;
using SIAG_CRATO.BLLs.Pallet;
using SIAG_CRATO.Repositories.Implementations;
using SIAG_CRATO.Repositories.Interfaces;
using SIAG_CRATO.Services;
using System.Data;

namespace SIAG_CRATO.Repositotories.Implementations;

public class AtividadeRotinaRepository : IAtividadeRotinaRepository
{
    private readonly IDbConnection _dbConnection;

    private string querySelectChamada = @"SELECT
            id_areaarmazenagemleitura   AS IdAreaArmazenagemLeitura,
            id_areaarmazenagemorigem    AS IdAreaArmazenagemOrigem,
            id_areaarmazenagemdestino   AS IdAreaArmazenagemDestino,
            id_palletleitura            AS IdPalletLeitura,
            id_palletorigem             AS IdPalletOrigem,
            id_operador                 AS IdOperador,
            id_atividade                AS IdAtividade
        FROM chamada
        WHERE id_chamada = @IdChamada;";

    private string queryHistoricoPallet = @"INSERT INTO historicopallet
            (dt_evento, id_pallet, id_atividade, id_responsabilidade, id_operador, id_chamada, id_areaarmazenagem)
        VALUES
            (GETDATE(), @IdPallet, @IdAtividade, @Responsabilidade, @IdOperador, @IdChamada, @IdAreaArmazenagem);";

    private string queryHistorico = @"
        INSERT INTO historico
            (dt_historico, nm_usuario, nm_objeto, id_registro, ds_operacao)
        VALUES
            (GETDATE(), @NmUsuario, 'pallet', @IdRegistro, @DsOperacao);";

    private int RESPONSABILIDADE_DEFAULT = 7508;

    private class HistoricoPalletDTO
    {
        public int IdPallet { get; set; }
        public int IdAtividade { get; set; }
        public int Responsabilidade { get; set; }
        public long? IdOperador { get; set; }
        public Guid IdChamada { get; set; }
        public long? IdAreaArmazenagem { get; set; }
    }

    public AtividadeRotinaRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
    }

    public async Task<ValidacaoEnderecoResult> AlocarPalletAsync(Guid idChamada)
    {

        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();

        EnderecoLeituraDto? chamadaDados = null;

        try
        {
            chamadaDados = await _dbConnection
                            .QuerySingleOrDefaultAsync<EnderecoLeituraDto>(querySelectChamada, new { IdChamada = idChamada })
                            .ConfigureAwait(false);

            // Se não encontrou a chamada, falha imediata:
            if (chamadaDados == null)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Chamada não encontrada."
                };
            }

            // 2) Verifica se id_areaarmazenagemleitura e id_palletleitura não são nulos E
            //    se id_areaarmazenagemorigem == id_areaarmazenagemleitura
            if (!chamadaDados.IdAreaArmazenagemLeitura.HasValue
                || !chamadaDados.IdPalletLeitura.HasValue
                || chamadaDados.IdAreaArmazenagemOrigem != chamadaDados.IdAreaArmazenagemLeitura.Value)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Você não está na posição correta!"
                };
            }

            // 3) Alocar o pallet (UPDATE na tabela 'pallet')
            const string sqlUpdatePallet = @"
UPDATE pallet
   SET id_areaarmazenagem = @IdAreaArmazenagemLeitura
 WHERE id_pallet = @IdPalletLeitura;";

            var parametrosUpdate = new
            {
                IdAreaArmazenagemLeitura = chamadaDados.IdAreaArmazenagemLeitura.Value,
                IdPalletLeitura = chamadaDados.IdPalletLeitura.Value
            };

            await _dbConnection.ExecuteAsync(sqlUpdatePallet, parametrosUpdate)
                .ConfigureAwait(false);

            var parametrosHistoricoPallet = new HistoricoPalletDTO
            {
                IdPallet = chamadaDados.IdPalletLeitura.Value,
                IdAtividade = chamadaDados.IdAtividade,
                Responsabilidade = RESPONSABILIDADE_DEFAULT,
                IdOperador = chamadaDados.IdOperador,
                IdChamada = idChamada,
                IdAreaArmazenagem = chamadaDados.IdAreaArmazenagemLeitura
            };
            await InsereHistoricoPallet(parametrosHistoricoPallet);

            var nmUsuario = chamadaDados.IdOperador.HasValue
                ? chamadaDados.IdOperador.Value.ToString()
                : string.Empty;

            var idRegistro = chamadaDados.IdPalletLeitura.Value.ToString();

            var dsOperacao = "Movimentação Chamada: atividade="
                             + chamadaDados.IdAtividade
                             + "&areaArmazenagem="
                             + chamadaDados.IdAreaArmazenagemLeitura.Value
                             + "&chamada="
                             + idChamada;

            var parametrosHistorico = new
            {
                NmUsuario = nmUsuario,
                IdRegistro = idRegistro,
                DsOperacao = dsOperacao
            };
            await _dbConnection.ExecuteAsync(queryHistorico, parametrosHistorico).ConfigureAwait(false);

            // 6) Se tudo ocorreu sem exceções “não capturadas”, devolvemos sucesso:
            return new ValidacaoEnderecoResult
            {
                IsValid = true,
                Mensagem = string.Empty
            };
        }
        catch { return null; }
        finally
        {
            if (_dbConnection.State == ConnectionState.Open)
                _dbConnection.Close();
        }
    }

    public async Task<ValidacaoEnderecoResult> AlocarPalletEnderecoAsync(Guid idChamada)
    {

        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();

        EnderecoLeituraDto? chamadaDados = null;

        try
        {
            chamadaDados = await _dbConnection
                            .QuerySingleOrDefaultAsync<EnderecoLeituraDto>(querySelectChamada, new { IdChamada = idChamada })
                            .ConfigureAwait(false);

            // Se não encontrou registro, devolvemos falha genérica (sem mensagem customizada definida na SP)
            if (chamadaDados == null)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Não foi possível ler a posição!" // corresponde ao ELSE final da SP
                };
            }

            // 2) Se não houver área de leitura, falhar imediatamente
            if (!chamadaDados.IdAreaArmazenagemLeitura.HasValue)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Não foi possível ler a posição!"
                };
            }

            // 3) Buscar id_endereco da área de leitura
            const string sqlSelectEnderecoLeitura = @"
SELECT id_endereco
FROM areaarmazenagem
WHERE id_areaarmazenagem = @IdAreaLeitura;
";
            var idEnderecoLeitura = await _dbConnection
                .QuerySingleOrDefaultAsync<int?>(
                    sqlSelectEnderecoLeitura,
                    new { IdAreaLeitura = chamadaDados.IdAreaArmazenagemLeitura.Value }
                )
                .ConfigureAwait(false);

            // 4) Buscar id_endereco da área de destino
            const string sqlSelectEnderecoDestino = @"
SELECT id_endereco
FROM areaarmazenagem
WHERE id_areaarmazenagem = @IdAreaDestino;
";
            var idEnderecoDestino = await _dbConnection
                .QuerySingleOrDefaultAsync<int?>(
                    sqlSelectEnderecoDestino,
                    new { IdAreaDestino = chamadaDados.IdAreaArmazenagemDestino }
                )
                .ConfigureAwait(false);

            // 5) Se os dois id_endereco forem iguais (e não nulos), prosseguir com alocação
            if (idEnderecoLeitura.HasValue
                && idEnderecoDestino.HasValue
                && idEnderecoLeitura.Value == idEnderecoDestino.Value)
            {
                // a) UPDATE na tabela 'pallet' usando IdPalletOrigem
                const string sqlUpdatePallet = @"
UPDATE pallet
   SET id_areaarmazenagem = @IdAreaLeitura
 WHERE id_pallet = @IdPalletOrigem;
";
                var parametrosUpdate = new
                {
                    IdAreaLeitura = chamadaDados.IdAreaArmazenagemLeitura.Value,
                    IdPalletOrigem = chamadaDados.IdPalletOrigem.Value
                };
                await _dbConnection.ExecuteAsync(sqlUpdatePallet, parametrosUpdate)
                    .ConfigureAwait(false);

                var parametrosHistoricoPallet = new HistoricoPalletDTO
                {
                    IdPallet = chamadaDados.IdPalletOrigem.Value,
                    IdAtividade = chamadaDados.IdAtividade,
                    Responsabilidade = RESPONSABILIDADE_DEFAULT,
                    IdOperador = chamadaDados.IdOperador,
                    IdChamada = idChamada,
                    IdAreaArmazenagem = chamadaDados.IdAreaArmazenagemLeitura.Value
                };
                await InsereHistoricoPallet(parametrosHistoricoPallet);

                // c) Inserir em historico
                var nmUsuario = chamadaDados.IdOperador.HasValue
                    ? chamadaDados.IdOperador.Value.ToString()
                    : string.Empty;

                var idRegistro = chamadaDados.IdPalletOrigem.Value.ToString();
                var dsOperacao = "Movimentação Chamada: atividade="
                                 + chamadaDados.IdAtividade
                                 + "&areaArmazenagem="
                                 + chamadaDados.IdAreaArmazenagemLeitura.Value
                                 + "&chamada="
                                 + idChamada;

                var parametrosHistorico = new
                {
                    NmUsuario = nmUsuario,
                    IdRegistro = idRegistro,
                    DsOperacao = dsOperacao
                };
                await _dbConnection.ExecuteAsync(queryHistorico, parametrosHistorico).ConfigureAwait(false);

                // 6) Sucesso: devolve IsValid=true e mensagem vazia
                return new ValidacaoEnderecoResult
                {
                    IsValid = true,
                    Mensagem = string.Empty
                };
            }
            else
            {
                // id_endereco_leitura ≠ id_endereco_destino
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Você não está no local correto!"
                };
            }
        }
        catch { return null; }
        finally
        {
            if (_dbConnection.State == ConnectionState.Open)
                _dbConnection.Close();
        }
    }

    public async Task<ValidacaoEnderecoResult> AlocarPalletLivreAsync(Guid idChamada)
    {
        const int STATUS_PALLET_LIVRE = 1;
        const int TIPOENDERECO_PP = 1;
        const int TIPOENDERECO_DOCA = 2;

        var parametrosSelect = new { IdChamada = idChamada };

        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();

        EnderecoLeituraDto? chamadaDados = null;

        try
        {
            chamadaDados = await _dbConnection
                .QuerySingleOrDefaultAsync<EnderecoLeituraDto>(querySelectChamada, parametrosSelect)
                .ConfigureAwait(false);

            // Se não encontrou registro, ou não há área de leitura, ou não há pallet de leitura,
            // ou origem != leitura → "Você não está na posição correta!"
            if (chamadaDados == null
                || !chamadaDados.IdAreaArmazenagemLeitura.HasValue
                || !chamadaDados.IdPalletLeitura.HasValue
                || chamadaDados.IdAreaArmazenagemOrigem != chamadaDados.IdAreaArmazenagemLeitura.Value)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Você não está na posição correta!"
                };
            }

            var idAreaLeitura = chamadaDados.IdAreaArmazenagemLeitura.Value;
            var idPalletLeitura = chamadaDados.IdPalletLeitura.Value;

            // 2) SELECT id_endereco da área lida
            const string sqlSelectEnderecoLeitura = @"
SELECT id_endereco
FROM areaarmazenagem WITH(NOLOCK)
WHERE id_areaarmazenagem = @IdAreaLeitura;
";
            var idEnderecoLeitura = await _dbConnection
                .QuerySingleOrDefaultAsync<int?>(
                    sqlSelectEnderecoLeitura,
                    new { IdAreaLeitura = idAreaLeitura }
                )
                .ConfigureAwait(false);

            // 3) Verifica EXISTE pallet livre em posição distinta do endereço lido
            const string sqlExistePalletLivre = @"
SELECT CASE WHEN EXISTS(
    SELECT 1
    FROM pallet WITH(NOLOCK)
    LEFT JOIN areaarmazenagem WITH(NOLOCK)
      ON areaarmazenagem.id_areaarmazenagem = pallet.id_areaarmazenagem
    WHERE pallet.id_pallet = @IdPalletLeitura
      AND pallet.fg_status = @StatusPalletLivre
      AND (
          areaarmazenagem.id_endereco IS NULL
          OR areaarmazenagem.id_endereco <> @IdEnderecoLeitura
      )
) THEN 1 ELSE 0 END;
";
            var existePalletLivre = await _dbConnection
                .QuerySingleAsync<int>(
                    sqlExistePalletLivre,
                    new
                    {
                        IdPalletLeitura = idPalletLeitura,
                        StatusPalletLivre = STATUS_PALLET_LIVRE,
                        IdEnderecoLeitura = idEnderecoLeitura
                    }
                )
                .ConfigureAwait(false);

            if (existePalletLivre == 0)
            {
                // "O pallet {idPalletLeitura} está em outra posição do sorter!"
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = $"O pallet {idPalletLeitura} está em outra posição do sorter!"
                };
            }

            // 4) Verifica NÃO EXISTE pallet armazenado em tipo PP/DOCA com status > livre
            const string sqlExistePalletArmazenado = @"
                SELECT CASE WHEN EXISTS(
                    SELECT 1
                    FROM pallet WITH(NOLOCK)
                    INNER JOIN areaarmazenagem WITH(NOLOCK)
                      ON areaarmazenagem.id_areaarmazenagem = pallet.id_areaarmazenagem
                    INNER JOIN endereco WITH(NOLOCK)
                      ON endereco.id_endereco = areaarmazenagem.id_endereco
                      AND endereco.id_tipoendereco IN (@TipoEnderecoPP, @TipoEnderecoDoca)
                    WHERE pallet.id_pallet = @IdPalletLeitura
                      AND pallet.fg_status > @StatusPalletLivre
                ) THEN 1 ELSE 0 END;
                ";
            var existeArmazenadoPPouDoca = await _dbConnection
                .QuerySingleAsync<int>(
                    sqlExistePalletArmazenado,
                    new
                    {
                        IdPalletLeitura = idPalletLeitura,
                        StatusPalletLivre = STATUS_PALLET_LIVRE,
                        TipoEnderecoPP = TIPOENDERECO_PP,
                        TipoEnderecoDoca = TIPOENDERECO_DOCA
                    }
                )
                .ConfigureAwait(false);

            if (existeArmazenadoPPouDoca == 1)
            {
                // "O pallet {idPalletLeitura} está armazenado!"
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = $"O pallet {idPalletLeitura} está armazenado!"
                };
            }

            // 5) Caso ambas condições anteriores sejam verdadeiras, prossegue:
            //    a) Limpa pallet do local atual: SET id_areaarmazenagem=NULL, fg_status=1, id_agrupador=NULL
            const string sqlLimpaPalletAtual = @"
                UPDATE pallet
                   SET id_areaarmazenagem = NULL,
                       fg_status = @StatusPalletLivre,
                       id_agrupador = NULL
                 WHERE id_areaarmazenagem = @IdAreaLeitura;
                ";
            await _dbConnection.ExecuteAsync(
                sqlLimpaPalletAtual,
                new
                {
                    StatusPalletLivre = STATUS_PALLET_LIVRE,
                    IdAreaLeitura = idAreaLeitura
                }
            ).ConfigureAwait(false);

            //    b) Aloca pallet lido: SET id_areaarmazenagem=@IdAreaLeitura, fg_status=1, id_agrupador=NULL
            const string sqlAlocaPallet = @"
                UPDATE pallet
                   SET id_areaarmazenagem = @IdAreaLeitura,
                       fg_status = @StatusPalletLivre,
                       id_agrupador = NULL
                 WHERE id_pallet = @IdPalletLeitura;
                ";
            await _dbConnection.ExecuteAsync(
                sqlAlocaPallet,
                new
                {
                    IdAreaLeitura = idAreaLeitura,
                    StatusPalletLivre = STATUS_PALLET_LIVRE,
                    IdPalletLeitura = idPalletLeitura
                }
            ).ConfigureAwait(false);

            var parametrosHistoricoPallet = new HistoricoPalletDTO
            {
                IdPallet = idPalletLeitura,
                IdAtividade = chamadaDados.IdAtividade,
                Responsabilidade = RESPONSABILIDADE_DEFAULT,
                IdOperador = chamadaDados.IdOperador,
                IdChamada = idChamada,
                IdAreaArmazenagem = idAreaLeitura
            };
            await InsereHistoricoPallet(parametrosHistoricoPallet);

            //    d) Inserir em historico
            var nmUsuario = chamadaDados.IdOperador.HasValue
                ? chamadaDados.IdOperador.Value.ToString()
                : string.Empty;

            var idRegistro = idPalletLeitura.ToString();
            var dsOperacao = "Movimentação Chamada: atividade="
                             + chamadaDados.IdAtividade
                             + "&areaArmazenagem="
                             + idAreaLeitura
                             + "&chamada="
                             + idChamada;

            var parametrosHistorico = new
            {
                NmUsuario = nmUsuario,
                IdRegistro = idRegistro,
                DsOperacao = dsOperacao
            };
            await _dbConnection.ExecuteAsync(queryHistorico, parametrosHistorico).ConfigureAwait(false);

            //    e) Limpar caixas: UPDATE caixa SET id_pallet = NULL WHERE id_pallet = @IdPalletLeitura
            const string sqlLimpaCaixas = @"
                UPDATE caixa
                   SET id_pallet = NULL
                 WHERE id_pallet = @IdPalletLeitura;
                ";
            await _dbConnection.ExecuteAsync(
                sqlLimpaCaixas,
                new { IdPalletLeitura = idPalletLeitura }
            ).ConfigureAwait(false);

            //    f) Sucesso: Mensagem vazia
            return new ValidacaoEnderecoResult
            {
                IsValid = true,
                Mensagem = string.Empty
            };
        }
        catch { return null; }
        finally
        {
            if (_dbConnection.State == ConnectionState.Open)
                _dbConnection.Close();
        }
    }

    public async Task<ValidacaoEnderecoResult> ArmazenarPalletAsync(Guid idChamada)
    {
        const int STATUS_AREA_LIVRE = 1;
        const int STATUS_AREA_OCUPADO = 3;

        var parametrosSelect = new { IdChamada = idChamada };

        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();

        EnderecoLeituraDto? chamadaDados = null;

        try
        {
            chamadaDados = await _dbConnection
                .QuerySingleOrDefaultAsync<EnderecoLeituraDto>(querySelectChamada, parametrosSelect)
                .ConfigureAwait(false);

            // Se não encontrou a chamada, devolvemos falha (apesar de a SP não tratar esse caso explicitamente)
            if (chamadaDados == null)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Chamada não encontrada."
                };
            }

            var idAreaOrigem = chamadaDados.IdAreaArmazenagemOrigem;
            var idAreaDestino = chamadaDados.IdAreaArmazenagemDestino;
            var idPalletOrigem = chamadaDados.IdPalletOrigem;
            var idOperador = chamadaDados.IdOperador;
            var idAtividade = chamadaDados.IdAtividade;

            // 2) Lógica de movimentação do pallet
            if (idPalletOrigem.HasValue)
            {
                // 2a) Atualizar pallet de origem para a área de destino
                const string sqlUpdatePalletPorId = @"
UPDATE pallet
   SET id_areaarmazenagem = @IdAreaDestino
 WHERE id_pallet = @IdPalletOrigem;
";
                await _dbConnection.ExecuteAsync(
                    sqlUpdatePalletPorId,
                    new { IdAreaDestino = idAreaDestino, IdPalletOrigem = idPalletOrigem.Value }
                ).ConfigureAwait(false);

                var parametrosHistoricoPallet = new HistoricoPalletDTO
                {
                    IdPallet = idPalletOrigem.Value,
                    IdAtividade = idAtividade,
                    Responsabilidade = RESPONSABILIDADE_DEFAULT,
                    IdOperador = idOperador,
                    IdChamada = idChamada,
                    IdAreaArmazenagem = idAreaDestino
                };
                await InsereHistoricoPallet(parametrosHistoricoPallet);

                // 2c) Gravar em historico
                var nmUsuario = idOperador.HasValue
                    ? idOperador.Value.ToString()
                    : string.Empty;

                var idRegistro = idPalletOrigem.Value.ToString();
                var dsOperacao = "Movimentação Chamada: atividade="
                                 + idAtividade
                                 + "&areaArmazenagem="
                                 + idAreaDestino
                                 + "&chamada="
                                 + idChamada;

                var parametrosHistorico = new
                {
                    NmUsuario = nmUsuario,
                    IdRegistro = idRegistro,
                    DsOperacao = dsOperacao
                };
                await _dbConnection.ExecuteAsync(queryHistorico, parametrosHistorico).ConfigureAwait(false);
            }
            else
            {
                // 2d) Se não houve pallet de origem informado, movimenta todos os pallets da área de origem para destino
                const string sqlUpdatePalletPorArea = @"
UPDATE pallet
   SET id_areaarmazenagem = @IdAreaDestino
 WHERE id_areaarmazenagem = @IdAreaOrigem;
";
                await _dbConnection.ExecuteAsync(
                    sqlUpdatePalletPorArea,
                    new { IdAreaDestino = idAreaDestino, IdAreaOrigem = idAreaOrigem }
                ).ConfigureAwait(false);
            }

            // 3) Marcar área de destino como ocupada
            const string sqlUpdateAreaDestino = @"
UPDATE areaarmazenagem
   SET fg_status = @StatusOcupado
 WHERE id_areaarmazenagem = @IdAreaDestino;
";
            await _dbConnection.ExecuteAsync(
                sqlUpdateAreaDestino,
                new { StatusOcupado = STATUS_AREA_OCUPADO, IdAreaDestino = idAreaDestino }
            ).ConfigureAwait(false);

            // 4) Marcar área de origem como livre
            const string sqlUpdateAreaOrigem = @"
UPDATE areaarmazenagem
   SET fg_status = @StatusLivre,
       id_agrupador = NULL
 WHERE id_areaarmazenagem = @IdAreaOrigem;
";
            await _dbConnection.ExecuteAsync(
                sqlUpdateAreaOrigem,
                new { StatusLivre = STATUS_AREA_LIVRE, IdAreaOrigem = idAreaOrigem }
            ).ConfigureAwait(false);

            // 5) Sucesso: devolve IsValid = true, Mensagem vazia
            return new ValidacaoEnderecoResult
            {
                IsValid = true,
                Mensagem = string.Empty
            };
        }
        catch { return null; }
        finally
        {
            if (_dbConnection.State == ConnectionState.Open)
                _dbConnection.Close();
        }
    }

    public async Task<ValidacaoEnderecoResult> LeituraAposCheioAsync(Guid idChamada)
    {
        // Constantes definidas na SP
        const int STATUS_AREA_LIVRE = 1;
        const string PARAM_STAGEIN = "TIPO AREA STAGEIN";

        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();

        EnderecoLeituraDto? chamadaDados = null;

        try
        {
            chamadaDados = await _dbConnection
                .QuerySingleOrDefaultAsync<EnderecoLeituraDto>(querySelectChamada, new { IdChamada = idChamada })
                .ConfigureAwait(false);

            if (chamadaDados == null)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Chamada não encontrada."
                };
            }

            var idPalletLeitura = chamadaDados.IdPalletLeitura;
            var idAreaLeitura = chamadaDados.IdAreaArmazenagemLeitura;
            var idOperador = chamadaDados.IdOperador;
            var idAtividade = chamadaDados.IdAtividade;

            if (!idAreaLeitura.HasValue)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Você não está na área de leitura"
                };
            }

            // 3) Obter código de tipoStageIn
            const string sqlSelectTipoStageIn = @"
SELECT CONVERT(int, nm_valor)
FROM parametro WITH(NOLOCK)
WHERE nm_parametro = @ParamStageIn;
";
            var tipoStageIn = await _dbConnection.QuerySingleOrDefaultAsync<int?>(
                sqlSelectTipoStageIn,
                new { ParamStageIn = PARAM_STAGEIN }
            ).ConfigureAwait(false);

            var enderecoBLL = new EnderecoBLL();
            var destinos = await enderecoBLL.ObterDestinoPalletAsync(idPalletLeitura ?? 0);

            // Extrair lista distinta de id_endereco
            var enderecos = destinos
                .Select(d => d)
                .Distinct()
                .ToList();

            // 5) Se não houver qualquer destino, log de EPRLB01 e retorna erro
            if (!enderecos.Any())
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "EPRLB01 - Lote não alocado no Armazém"
                };
            }

            // 6) Verificar existência de área Stage-In livre
            const string sqlVerificaStageIn = @"
SELECT TOP 1 1
FROM areaarmazenagem WITH(NOLOCK)
WHERE id_endereco IN @Enderecos
  AND id_tipoarea = @TipoStageIn
  AND fg_status = @StatusLivre;
";
            var existeStageInLivre = await _dbConnection.QuerySingleOrDefaultAsync<int?>(
                sqlVerificaStageIn,
                new
                {
                    Enderecos = enderecos,
                    TipoStageIn = tipoStageIn,
                    StatusLivre = STATUS_AREA_LIVRE
                }
            ).ConfigureAwait(false);

            if (!existeStageInLivre.HasValue)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "EPRLB02 - Nenhuma posição de stage-in disponível"
                };
            }

            // 7) Buscar idAreaOrigem do pallet
            const string sqlSelectAreaOrigemPallet = @"
SELECT id_areaarmazenagem
FROM pallet WITH(NOLOCK)
WHERE id_pallet = @IdPalletLeitura;
";
            var idAreaOrigem = await _dbConnection.QuerySingleOrDefaultAsync<long?>(
                sqlSelectAreaOrigemPallet,
                new { IdPalletLeitura = idPalletLeitura }
            ).ConfigureAwait(false);

            var parametrosHistoricoPallet = new HistoricoPalletDTO
            {
                IdPallet = idPalletLeitura ?? 0,
                IdAtividade = idAtividade,
                Responsabilidade = RESPONSABILIDADE_DEFAULT,
                IdOperador = idOperador,
                IdChamada = idChamada,
                IdAreaArmazenagem = idAreaLeitura
            };
            await InsereHistoricoPallet(parametrosHistoricoPallet);

            // 9) Registrar histórico em 'historico'
            var nmUsuario = idOperador.HasValue
                ? idOperador.Value.ToString()
                : string.Empty;

            var dsOperacao = "Movimentação Chamada: atividade="
                             + idAtividade
                             + "&areaArmazenagem="
                             + idAreaLeitura
                             + "&chamada="
                             + idChamada;

            var parametrosHistorico = new
            {
                NmUsuario = nmUsuario,
                IdRegistro = idPalletLeitura.ToString(),
                DsOperacao = dsOperacao
            };
            await _dbConnection.ExecuteAsync(queryHistorico, parametrosHistorico).ConfigureAwait(false);

            // 10) Atualizar chamada: definir origem e destino conforme pallet de leitura
            const string sqlUpdateChamada = @"
UPDATE chamada
   SET id_palletorigem            = id_palletleitura,
       id_areaarmazenagemorigem   = @IdAreaOrigem,
       id_palletdestino           = id_palletleitura,
       id_areaarmazenagemdestino  = NULL
 WHERE id_chamada = @IdChamada;
";
            await _dbConnection.ExecuteAsync(
                sqlUpdateChamada,
                new
                {
                    IdAreaOrigem = idAreaOrigem,
                    IdChamada = idChamada
                }
            ).ConfigureAwait(false);

            // Sucesso
            return new ValidacaoEnderecoResult
            {
                IsValid = true,
                Mensagem = string.Empty
            };
        }
        catch { return null; }
        finally
        {
            if (_dbConnection.State == ConnectionState.Open)
                _dbConnection.Close();
        }
    }

    public async Task<ValidacaoEnderecoResult> LeituraStageInAsync(Guid idChamada)
    {
        const byte STATUS_AREA_OCUPADO = 3;
        const byte STATUS_PALLET_OCUPADO = 3;

        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();

        EnderecoLeituraDto? chamadaDados = null;

        try
        {
            chamadaDados = await _dbConnection
                            .QuerySingleOrDefaultAsync<EnderecoLeituraDto>(querySelectChamada, new { IdChamada = idChamada })
                            .ConfigureAwait(false);

            if (chamadaDados == null)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Chamada não encontrada."
                };
            }

            var idAreaOrigem = chamadaDados.IdAreaArmazenagemOrigem;
            var idAreaLeitura = chamadaDados.IdAreaArmazenagemLeitura;
            var idPallet = chamadaDados.IdPalletLeitura;
            var idOperador = chamadaDados.IdOperador;
            var idAtividade = chamadaDados.IdAtividade;

            // 2) Verificar se a área de leitura coincide com a área de origem
            if (!idAreaLeitura.HasValue || idAreaOrigem != idAreaLeitura.Value)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Você não está na posição correta"
                };
            }

            // 3) Mover pallet para a área de origem e marcar como ocupado
            const string sqlUpdatePallet = @"
UPDATE pallet
   SET id_areaarmazenagem = @IdAreaOrigem,
       fg_status = @StatusPalletOcupado
 WHERE id_pallet = @IdPallet;
";
            await _dbConnection.ExecuteAsync(
                sqlUpdatePallet,
                new
                {
                    IdAreaOrigem = idAreaOrigem,
                    StatusPalletOcupado = STATUS_PALLET_OCUPADO,
                    IdPallet = idPallet
                }
            ).ConfigureAwait(false);

            var parametrosHistoricoPallet = new HistoricoPalletDTO
            {
                IdPallet = idPallet ?? 0,
                IdAtividade = idAtividade,
                Responsabilidade = RESPONSABILIDADE_DEFAULT,
                IdOperador = idOperador,
                IdChamada = idChamada,
                IdAreaArmazenagem = idAreaOrigem
            };
            await InsereHistoricoPallet(parametrosHistoricoPallet);

            // 4b) Inserir em historico
            var nmUsuario = idOperador.HasValue
                ? idOperador.Value.ToString()
                : string.Empty;

            var dsOperacao = "Movimentação Chamada: atividade="
                             + idAtividade
                             + "&areaArmazenagem="
                             + idAreaOrigem
                             + "&chamada="
                             + idChamada;

            var parametrosHistorico = new
            {
                NmUsuario = nmUsuario,
                IdRegistro = idPallet.ToString(),
                DsOperacao = dsOperacao
            };
            await _dbConnection.ExecuteAsync(queryHistorico, parametrosHistorico).ConfigureAwait(false);

            // 5) Marcar a área de origem como ocupada
            const string sqlUpdateArea = @"
UPDATE areaarmazenagem
   SET fg_status = @StatusAreaOcupado
 WHERE id_areaarmazenagem = @IdAreaOrigem;
";
            await _dbConnection.ExecuteAsync(
                sqlUpdateArea,
                new
                {
                    StatusAreaOcupado = STATUS_AREA_OCUPADO,
                    IdAreaOrigem = idAreaOrigem
                }
            ).ConfigureAwait(false);

            // 6) Obter informações de endereço, posição Y e lado para montar a mensagem
            string mensagem;
            try
            {
                const string sqlSelectAreaInfo = @"
SELECT id_endereco AS IdEndereco, nr_posicaoy AS NrPosY, nr_lado AS NrLado
FROM areaarmazenagem
WHERE id_areaarmazenagem = @IdAreaOrigem;
";
                var areaInfo = await _dbConnection.QuerySingleOrDefaultAsync<AreaInfoDto>(
                    sqlSelectAreaInfo,
                    new { IdAreaOrigem = idAreaOrigem }
                ).ConfigureAwait(false);

                if (areaInfo != null)
                {
                    const string sqlSelectEndereco = @"
SELECT ISNULL(nm_endereco, '') AS NmEndereco
FROM endereco
WHERE id_endereco = @IdEndereco;
";
                    var nmEndereco = await _dbConnection.QuerySingleOrDefaultAsync<string>(
                        sqlSelectEndereco,
                        new { areaInfo.IdEndereco }
                    ).ConfigureAwait(false);

                    mensagem = $"{nmEndereco}, A: {areaInfo.NrPosY} L: {areaInfo.NrLado}";
                }
                else
                {
                    mensagem = string.Empty;
                }
            }
            catch
            {
                mensagem = string.Empty;
            }

            return new ValidacaoEnderecoResult
            {
                IsValid = true,
                Mensagem = mensagem
            };
        }
        catch { return null; }
        finally
        {
            if (_dbConnection.State == ConnectionState.Open)
                _dbConnection.Close();
        }
    }

    public async Task<ValidacaoEnderecoResult> VerificaDesalocaPalletOrigemAsync(Guid idChamada)
    {
        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();

        EnderecoLeituraDto? chamadaDados = null;

        try
        {
            chamadaDados = await _dbConnection
                            .QuerySingleOrDefaultAsync<EnderecoLeituraDto>(querySelectChamada, new { IdChamada = idChamada })
                            .ConfigureAwait(false);

            if (chamadaDados == null)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Chamada não encontrada."
                };
            }

            var idPalletOrigem = chamadaDados.IdPalletOrigem;
            var idPalletLeitura = chamadaDados.IdPalletLeitura;
            var idOperador = chamadaDados.IdOperador;
            var idAtividade = chamadaDados.IdAtividade;
            var idAreaOrigem = chamadaDados.IdAreaArmazenagemOrigem;

            // 2) Se id_palletorigem for null OU igual a id_palletleitura
            if (!idPalletOrigem.HasValue || idPalletOrigem.Value == idPalletLeitura)
            {
                // 2a) Atualizar pallet para id_areaarmazenagem = NULL
                const string sqlUpdatePallet = @"
UPDATE pallet
   SET id_areaarmazenagem = NULL
 WHERE id_pallet = @IdPalletOrigem;
";
                await _dbConnection.ExecuteAsync(
                    sqlUpdatePallet,
                    new { IdPalletOrigem = idPalletOrigem }
                ).ConfigureAwait(false);

                var parametrosHistoricoPallet = new HistoricoPalletDTO
                {
                    IdPallet = idPalletOrigem ?? 0,
                    IdAtividade = idAtividade,
                    Responsabilidade = RESPONSABILIDADE_DEFAULT,
                    IdOperador = idOperador,
                    IdChamada = idChamada,
                    IdAreaArmazenagem = idAreaOrigem
                };
                await InsereHistoricoPallet(parametrosHistoricoPallet);

                // 2c) Inserir em historico
                var nmUsuario = idOperador.HasValue
                    ? idOperador.Value.ToString()
                    : string.Empty;

                var idRegistro = idPalletOrigem.HasValue
                    ? idPalletOrigem.Value.ToString()
                    : string.Empty;

                var dsOperacao = "Movimentação Chamada: atividade="
                                 + idAtividade
                                 + "&areaArmazenagem="
                                 + string.Empty
                                 + "&chamada="
                                 + idChamada;

                var parametrosHistorico = new
                {
                    NmUsuario = nmUsuario,
                    IdRegistro = idRegistro,
                    DsOperacao = dsOperacao
                };
                await _dbConnection.ExecuteAsync(queryHistorico, parametrosHistorico).ConfigureAwait(false);

                // 2d) Retornar sucesso
                return new ValidacaoEnderecoResult
                {
                    IsValid = true,
                    Mensagem = string.Empty
                };
            }

            // 3) Caso contrário, falha com mensagem específica
            return new ValidacaoEnderecoResult
            {
                IsValid = false,
                Mensagem = $"O pallet {idPalletLeitura} não é o esperado!"
            };
        }
        catch { return null; }
        //finally
        //{
        //    if (_dbConnection.State == ConnectionState.Open)
        //        _dbConnection.Close();
        //}
    }

    public Task<ValidacaoEnderecoResult> SempreOkAsync(Guid idChamada)
    {
        return Task.FromResult(new ValidacaoEnderecoResult
        {
            IsValid = true,
            Mensagem = string.Empty
        });
    }

    private async Task InsereHistoricoPallet(HistoricoPalletDTO dto)
    {
        try
        {
            await _dbConnection.ExecuteAsync(queryHistoricoPallet, dto).ConfigureAwait(false);
        }
        catch { }
    }
}