using Dapper;
using SIAG_CRATO.Repositories.Interfaces;
using SIAG_CRATO.Services;
using System.Data;

namespace SIAG_CRATO.Repositotories.Implementations;

public class AtividadeRotinaRepository : IAtividadeRotinaRepository
{
    private readonly IDbConnection _dbConnection;


    public AtividadeRotinaRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
    }

    public async Task<ValidacaoEnderecoResult> AlocarPalletAsync(Guid idChamada)
    {
        const int RESPONSABILIDADE_DEFAULT = 7508;

        // 1) Buscamos os campos necessários da tabela 'chamada'
        const string sqlSelect = @"
SELECT
    id_areaarmazenagemleitura   AS IdAreaArmazenagemLeitura,
    id_palletleitura            AS IdPalletLeitura,
    id_areaarmazenagemorigem    AS IdAreaArmazenagemOrigem,
    id_operador                 AS IdOperador,
    id_atividade                AS IdAtividade
FROM chamada
WHERE id_chamada = @IdChamada;";

        var parametrosSelect = new { IdChamada = idChamada };

        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();

        EnderecoLeituraEPrimarioDto chamadaDados = null;

        try
        {
            chamadaDados = await _dbConnection
                .QuerySingleOrDefaultAsync<EnderecoLeituraEPrimarioDto>(sqlSelect, parametrosSelect)
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

            // 4) Registrar historico do pallet (IN para 'historicopallet')
            //    Colocamos em try/catch para "silenciar" possíveis erros, conforme a procedure original
            try
            {
                const string sqlInsertHistoricoPallet = @"
INSERT INTO historicopallet
    (dt_evento, id_pallet, id_atividade, id_responsabilidade, id_operador, id_chamada, id_areaarmazenagem)
VALUES
    (GETDATE(), @IdPalletLeitura, @IdAtividade, @Responsabilidade, @IdOperador, @IdChamada, @IdAreaArmazenagemLeitura);";

                var parametrosHistoricoPallet = new
                {
                    IdPalletLeitura = chamadaDados.IdPalletLeitura.Value,
                    chamadaDados.IdAtividade,
                    Responsabilidade = RESPONSABILIDADE_DEFAULT,
                    chamadaDados.IdOperador,       // pode ser null, tudo bem
                    IdChamada = idChamada,
                    IdAreaArmazenagemLeitura = chamadaDados.IdAreaArmazenagemLeitura.Value
                };

                await _dbConnection.ExecuteAsync(sqlInsertHistoricoPallet, parametrosHistoricoPallet)
                    .ConfigureAwait(false);
            }
            catch
            {
                // Conforme a procedure: “BEGIN CATCH ... -- Deu ruim, parou”
                // Simplesmente “silencia” qualquer falha aqui e continua.
            }

            // 5) Inserir registro em 'historico' (IN para 'historico')
            //    Precisamos formar:
            //      nm_usuario = id_operador.ToString()  OU vazio se null
            //      nm_objeto  = 'pallet'
            //      id_registro = id_palletleitura.ToString()
            //      ds_operacao = "Movimentação Chamada: atividade={id_atividade}&areaArmazenagem={id_areaLeitura}&chamada={idChamada}"
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

            const string sqlInsertHistorico = @"
INSERT INTO historico
    (dt_historico, nm_usuario, nm_objeto, id_registro, ds_operacao)
VALUES
    (GETDATE(), @NmUsuario, 'pallet', @IdRegistro, @DsOperacao);";

            var parametrosHistorico = new
            {
                NmUsuario = nmUsuario,
                IdRegistro = idRegistro,
                DsOperacao = dsOperacao
            };

            await _dbConnection.ExecuteAsync(sqlInsertHistorico, parametrosHistorico)
                .ConfigureAwait(false);

            // 6) Se tudo ocorreu sem exceções “não capturadas”, devolvemos sucesso:
            return new ValidacaoEnderecoResult
            {
                IsValid = true,
                Mensagem = string.Empty
            };
        }
        finally
        {
            if (_dbConnection.State == ConnectionState.Open)
                _dbConnection.Close();
        }
    }

    public async Task<ValidacaoEnderecoResult> AlocarPalletEnderecoAsync(Guid idChamada)
    {
        const int RESPONSABILIDADE_DEFAULT = 7508;

        // 1) Recuperar todos os campos necessários da tabela 'chamada'
        const string sqlSelectChamada = @"
SELECT
    id_areaarmazenagemleitura   AS IdAreaArmazenagemLeitura,
    id_areaarmazenagemdestino   AS IdAreaArmazenagemDestino,
    id_palletleitura            AS IdPalletLeitura,
    id_palletorigem             AS IdPalletOrigem,
    id_operador                 AS IdOperador,
    id_atividade                AS IdAtividade
FROM chamada
WHERE id_chamada = @IdChamada;
";
        var parametrosSelect = new { IdChamada = idChamada };

        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();

        EnderecoLeituraEAlocacaoDto chamadaDados = null;

        try
        {
            chamadaDados = await _dbConnection
                .QuerySingleOrDefaultAsync<EnderecoLeituraEAlocacaoDto>(sqlSelectChamada, parametrosSelect)
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

                // b) Inserir em historicopallet (silenciando erros, conforme BEGIN CATCH da SP)
                try
                {
                    const string sqlInsertHistoricoPallet = @"
INSERT INTO historicopallet
    (dt_evento, id_pallet, id_atividade, id_responsabilidade, id_operador, id_chamada, id_areaarmazenagem)
VALUES
    (GETDATE(), @IdPalletOrigem, @IdAtividade, @Responsabilidade, @IdOperador, @IdChamada, @IdAreaLeitura);
";
                    var parametrosHistoricoPallet = new
                    {
                        IdPalletOrigem = chamadaDados.IdPalletOrigem.Value,
                        chamadaDados.IdAtividade,
                        Responsabilidade = RESPONSABILIDADE_DEFAULT,
                        chamadaDados.IdOperador,
                        IdChamada = idChamada,
                        IdAreaLeitura = chamadaDados.IdAreaArmazenagemLeitura.Value
                    };
                    await _dbConnection.ExecuteAsync(sqlInsertHistoricoPallet, parametrosHistoricoPallet)
                        .ConfigureAwait(false);
                }
                catch
                {
                    // “Deu ruim, parou” — ignora qualquer falha no insert em historicopallet.
                }

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

                const string sqlInsertHistorico = @"
INSERT INTO historico
    (dt_historico, nm_usuario, nm_objeto, id_registro, ds_operacao)
VALUES
    (GETDATE(), @NmUsuario, 'pallet', @IdRegistro, @DsOperacao);
";
                var parametrosHistorico = new
                {
                    NmUsuario = nmUsuario,
                    IdRegistro = idRegistro,
                    DsOperacao = dsOperacao
                };
                await _dbConnection.ExecuteAsync(sqlInsertHistorico, parametrosHistorico)
                    .ConfigureAwait(false);

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
        const int RESPONSABILIDADE_DEFAULT = 7508;

        // 1) SELECT campos de 'chamada'
        const string sqlSelectChamada = @"
SELECT
    id_areaarmazenagemleitura   AS IdAreaArmazenagemLeitura,
    id_palletleitura            AS IdPalletLeitura,
    id_areaarmazenagemorigem    AS IdAreaArmazenagemOrigem,
    id_operador                 AS IdOperador,
    id_atividade                AS IdAtividade
FROM chamada WITH(NOLOCK)
WHERE id_chamada = @IdChamada;
";
        var parametrosSelect = new { IdChamada = idChamada };

        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();

        ChamadaLivreDto chamadaDados = null;

        try
        {
            chamadaDados = await _dbConnection
                .QuerySingleOrDefaultAsync<ChamadaLivreDto>(sqlSelectChamada, parametrosSelect)
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

            //    c) Gravar em historicopallet (silenciando erros)
            try
            {
                const string sqlInsertHistoricoPallet = @"
INSERT INTO historicopallet
    (dt_evento, id_pallet, id_atividade, id_responsabilidade, id_operador, id_chamada, id_areaarmazenagem)
VALUES
    (GETDATE(), @IdPalletLeitura, @IdAtividade, @Responsabilidade, @IdOperador, @IdChamada, @IdAreaLeitura);
";
                await _dbConnection.ExecuteAsync(
                    sqlInsertHistoricoPallet,
                    new
                    {
                        IdPalletLeitura = idPalletLeitura,
                        chamadaDados.IdAtividade,
                        Responsabilidade = RESPONSABILIDADE_DEFAULT,
                        chamadaDados.IdOperador,
                        IdChamada = idChamada,
                        IdAreaLeitura = idAreaLeitura
                    }
                ).ConfigureAwait(false);
            }
            catch
            {
                // “Deu ruim, parou” – ignora falhas na inserção em historicopallet
            }

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

            const string sqlInsertHistorico = @"
INSERT INTO historico
    (dt_historico, nm_usuario, nm_objeto, id_registro, ds_operacao)
VALUES
    (GETDATE(), @NmUsuario, 'pallet', @IdRegistro, @DsOperacao);
";
            await _dbConnection.ExecuteAsync(
                sqlInsertHistorico,
                new
                {
                    NmUsuario = nmUsuario,
                    IdRegistro = idRegistro,
                    DsOperacao = dsOperacao
                }
            ).ConfigureAwait(false);

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
        const int RESPONSABILIDADE_DEFAULT = 7508;

        // 1) Recuperar dados da 'chamada'
        const string sqlSelectChamada = @"
SELECT
    id_areaarmazenagemorigem AS IdAreaOrigem,
    id_areaarmazenagemdestino AS IdAreaDestino,
    id_palletorigem         AS IdPalletOrigem,
    id_operador             AS IdOperador,
    id_atividade            AS IdAtividade
FROM chamada
WHERE id_chamada = @IdChamada;
";
        var parametrosSelect = new { IdChamada = idChamada };

        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();

        ArmazenarPalletDto chamadaDados = null;

        try
        {
            chamadaDados = await _dbConnection
                .QuerySingleOrDefaultAsync<ArmazenarPalletDto>(sqlSelectChamada, parametrosSelect)
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

            var idAreaOrigem = chamadaDados.IdAreaOrigem;
            var idAreaDestino = chamadaDados.IdAreaDestino;
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

                // 2b) Gravar histórico em historicopallet (silenciando erros, conforme o BEGIN CATCH da SP)
                try
                {
                    const string sqlInsertHistoricoPallet = @"
INSERT INTO historicopallet
    (dt_evento, id_pallet, id_atividade, id_responsabilidade, id_operador, id_chamada, id_areaarmazenagem)
VALUES
    (GETDATE(), @IdPalletOrigem, @IdAtividade, @Responsabilidade, @IdOperador, @IdChamada, @IdAreaDestino);
";
                    await _dbConnection.ExecuteAsync(
                        sqlInsertHistoricoPallet,
                        new
                        {
                            IdPalletOrigem = idPalletOrigem.Value,
                            IdAtividade = idAtividade,
                            Responsabilidade = RESPONSABILIDADE_DEFAULT,
                            IdOperador = idOperador,
                            IdChamada = idChamada,
                            IdAreaDestino = idAreaDestino
                        }
                    ).ConfigureAwait(false);
                }
                catch
                {
                    // “Deu ruim mesmo” – ignora qualquer falha aqui
                }

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

                const string sqlInsertHistorico = @"
INSERT INTO historico
    (dt_historico, nm_usuario, nm_objeto, id_registro, ds_operacao)
VALUES
    (GETDATE(), @NmUsuario, 'pallet', @IdRegistro, @DsOperacao);
";
                await _dbConnection.ExecuteAsync(
                    sqlInsertHistorico,
                    new
                    {
                        NmUsuario = nmUsuario,
                        IdRegistro = idRegistro,
                        DsOperacao = dsOperacao
                    }
                ).ConfigureAwait(false);
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
        finally
        {
            if (_dbConnection.State == ConnectionState.Open)
                _dbConnection.Close();
        }
    }

    public async Task<ValidacaoEnderecoResult> DefinirExpedicaoAsync(Guid idChamada)
    {
        const int STATUS_AREA_LIVRE = 1;
        const int STATUS_AREA_RESERVADO = 2;
        const int STATUS_AREA_OCUPADO = 3;
        const string PARAM_STAGEOUT = "TIPO AREA STAGEOUT";

        const int STATUS_ORDEM_ALOCADA = 12;
        const int STATUS_ORDEM_CONFERIDA = 13;
        const int STATUS_ORDEM_AGUARDANDO_EXPEDICAO = 21;
        const int STATUS_ORDEM_EXPEDICAO = 22;

        long responsabilidade = 7508;

        // 1) SELECT dados iniciais da 'chamada'
        const string sqlSelectChamada = @"
SELECT
    id_areaarmazenagemorigem AS IdAreaOrigem,
    id_areaarmazenagemdestino AS IdAreaDestino,
    id_palletorigem         AS IdPalletOrigem,
    id_operador             AS IdOperador,
    id_atividade            AS IdAtividade
FROM chamada
WHERE id_chamada = @IdChamada;
";
        var parametrosChamada = new { IdChamada = idChamada };

        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();

        DefinirExpedicaoDto chamadaDados = null;

        try
        {
            chamadaDados = await _dbConnection
                .QuerySingleOrDefaultAsync<DefinirExpedicaoDto>(sqlSelectChamada, parametrosChamada)
                .ConfigureAwait(false);

            if (chamadaDados == null)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Chamada não encontrada."
                };
            }

            var idAreaOrigem = chamadaDados.IdAreaOrigem;
            var idAreaDestino = chamadaDados.IdAreaDestino;
            var idPalletOrigem = chamadaDados.IdPalletOrigem;
            var idOperador = chamadaDados.IdOperador;
            var idAtividade = chamadaDados.IdAtividade;

            // 2) Buscar área auxiliar (stage-out) em tmp_transicaochamada
            const string sqlSelectAreaAux = @"
SELECT id_areaarmazenagemdestino
FROM tmp_transicaochamada
WHERE id_areaarmazenagemorigem = @IdAreaDestino;
";
            var idAreaAux = await _dbConnection
                .QuerySingleOrDefaultAsync<long?>(
                    sqlSelectAreaAux,
                    new { IdAreaDestino = idAreaDestino }
                )
                .ConfigureAwait(false);

            // 3) Se não encontrou área auxiliar, retorna falha
            if (!idAreaAux.HasValue)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Não foi encontrado destino do pallet"
                };
            }

            // 4a) Buscar id_endereco da área auxiliar
            const string sqlSelectIdEndereco = @"
SELECT id_endereco
FROM areaarmazenagem WITH(NOLOCK)
WHERE id_areaarmazenagem = @IdAreaAux;
";
            var idEndereco = await _dbConnection
                .QuerySingleOrDefaultAsync<long?>(
                    sqlSelectIdEndereco,
                    new { IdAreaAux = idAreaAux.Value }
                )
                .ConfigureAwait(false);

            // 4b) Definir responsabilidade a partir de 'ordem', se houver registro
            if (idEndereco.HasValue)
            {
                const string sqlSelectResponsabilidade = @"
SELECT TOP 1 id_ordem
FROM ordem WITH(NOLOCK)
WHERE id_endereco = @IdEndereco
  AND fg_status IN (@StatusAlocada, @StatusExpedicao, @StatusConferida, @StatusAguardandoExpedicao)
ORDER BY fg_status DESC, dt_geracao DESC;
";
                var possívelResp = await _dbConnection
                    .QuerySingleOrDefaultAsync<long?>(
                        sqlSelectResponsabilidade,
                        new
                        {
                            IdEndereco = idEndereco.Value,
                            StatusAlocada = STATUS_ORDEM_ALOCADA,
                            StatusExpedicao = STATUS_ORDEM_EXPEDICAO,
                            StatusConferida = STATUS_ORDEM_CONFERIDA,
                            StatusAguardandoExpedicao = STATUS_ORDEM_AGUARDANDO_EXPEDICAO
                        }
                    )
                    .ConfigureAwait(false);

                if (possívelResp.HasValue)
                {
                    responsabilidade = possívelResp.Value;
                }
                // se não encontrou, mantém 7508
            }

            // 5) Atualizar 'chamada': origem passa a ser área de destino; destino passa a ser área auxiliar
            const string sqlUpdateChamada = @"
UPDATE chamada
   SET id_areaarmazenagemorigem = @IdAreaDestino,
       id_areaarmazenagemdestino = @IdAreaAux
 WHERE id_chamada = @IdChamada;
";
            await _dbConnection.ExecuteAsync(
                sqlUpdateChamada,
                new
                {
                    IdAreaDestino = idAreaDestino,
                    IdAreaAux = idAreaAux.Value,
                    IdChamada = idChamada
                }
            ).ConfigureAwait(false);

            // 6) Inserir em historicopallet (silenciando erros, conforme BEGIN CATCH da SP)
            try
            {
                const string sqlInsertHistoricoPallet = @"
INSERT INTO historicopallet
    (dt_evento, id_pallet, id_atividade, id_responsabilidade, id_operador, id_chamada, id_areaarmazenagem)
VALUES
    (GETDATE(), @IdPalletOrigem, @IdAtividade, @Responsabilidade, @IdOperador, @IdChamada, @IdAreaDestino);
";
                await _dbConnection.ExecuteAsync(
                    sqlInsertHistoricoPallet,
                    new
                    {
                        IdPalletOrigem = idPalletOrigem,
                        IdAtividade = idAtividade,
                        Responsabilidade = responsabilidade,
                        IdOperador = idOperador,
                        IdChamada = idChamada,
                        IdAreaDestino = idAreaDestino
                    }
                ).ConfigureAwait(false);
            }
            catch
            {
                // “Deu ruim mesmo” – ignora qualquer falha aqui
            }

            // 7) Sucesso
            return new ValidacaoEnderecoResult
            {
                IsValid = true,
                Mensagem = string.Empty
            };
        }
        finally
        {
            if (_dbConnection.State == ConnectionState.Open)
                _dbConnection.Close();
        }
    }

    public async Task<ValidacaoEnderecoResult> DefinirStageOutAsync(Guid idChamada)
    {
        const int STATUS_AREA_LIVRE = 1;
        const int STATUS_AREA_RESERVADO = 2;
        const string PARAM_STAGEOUT = "TIPO AREA STAGEOUT";

        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();

        try
        {
            // 1) Obter o valor inteiro de PARAM_STAGEOUT
            const string sqlSelectTipo = @"
SELECT CONVERT(int, nm_valor) 
FROM parametro 
WHERE nm_parametro = @ParamStageOut;
";
            var tipoStageOut = await _dbConnection
                .QuerySingleOrDefaultAsync<int?>(
                    sqlSelectTipo,
                    new { ParamStageOut = PARAM_STAGEOUT }
                )
                .ConfigureAwait(false);

            // Se não encontrar o parâmetro, segue adiante; possivelmente não haverá área Stage-Out livre
            // (o SELECT de areaarmazenagem a seguir retornará null em idAreaAux)

            // 2) Recuperar dados da chamada e da área de origem
            const string sqlSelectChamada = @"
SELECT
    c.id_areaarmazenagemorigem       AS IdAreaOrigem,
    c.id_areaarmazenagemdestino      AS IdAreaDestino,
    aa.id_endereco                   AS IdEnderecoOrigem,
    aa.id_agrupador                  AS IdAgrupadorOrigem
FROM chamada AS c
INNER JOIN areaarmazenagem AS aa
    ON aa.id_areaarmazenagem = c.id_areaarmazenagemorigem
WHERE c.id_chamada = @IdChamada;
";
            var chamadaDados = await _dbConnection
                .QuerySingleOrDefaultAsync<DefinirStageOutDto>(
                    sqlSelectChamada,
                    new { IdChamada = idChamada }
                )
                .ConfigureAwait(false);

            if (chamadaDados == null)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Chamada não encontrada."
                };
            }

            var idAreaDestino = chamadaDados.IdAreaDestino;
            var idEnderecoOrigem = chamadaDados.IdEnderecoOrigem;
            var idAgrupadorOrigem = chamadaDados.IdAgrupadorOrigem;

            // 3) Buscar uma área Stage-Out livre no mesmo endereço de origem
            const string sqlSelectAreaAux = @"
SELECT TOP 1 id_areaarmazenagem
FROM areaarmazenagem
WHERE id_endereco = @IdEnderecoOrigem
  AND id_tipoarea = @TipoStageOut
  AND fg_status = @StatusLivre;
";
            var idAreaAux = await _dbConnection
                .QuerySingleOrDefaultAsync<long?>(
                    sqlSelectAreaAux,
                    new
                    {
                        IdEnderecoOrigem = idEnderecoOrigem,
                        TipoStageOut = tipoStageOut,
                        StatusLivre = STATUS_AREA_LIVRE
                    }
                )
                .ConfigureAwait(false);

            // 4) Se não houver área auxiliar disponível
            if (!idAreaAux.HasValue)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Nenhuma posição de stage-out disponível"
                };
            }

            // 5) Reservar a área Stage-Out (status reservado e id_agrupador = agrupador da chamada)
            const string sqlUpdateAreaAux = @"
UPDATE areaarmazenagem
   SET fg_status   = @StatusReservado,
       id_agrupador = @IdAgrupador
 WHERE id_areaarmazenagem = @IdAreaAux;
";
            await _dbConnection.ExecuteAsync(
                sqlUpdateAreaAux,
                new
                {
                    StatusReservado = STATUS_AREA_RESERVADO,
                    IdAgrupador = idAgrupadorOrigem,
                    IdAreaAux = idAreaAux.Value
                }
            ).ConfigureAwait(false);

            // 6) Apagar registros anteriores em tmp_transicaochamada para esta área auxiliar
            const string sqlDeleteTemp = @"
DELETE FROM tmp_transicaochamada
WHERE id_areaarmazenagemorigem = @IdAreaAux;
";
            await _dbConnection.ExecuteAsync(
                sqlDeleteTemp,
                new { IdAreaAux = idAreaAux.Value }
            ).ConfigureAwait(false);

            // 7) Inserir novo registro de transição: (origem = área auxiliar, destino = área destino)
            const string sqlInsertTemp = @"
INSERT INTO tmp_transicaochamada
    (id_areaarmazenagemorigem, id_areaarmazenagemdestino)
VALUES
    (@IdAreaAux, @IdAreaDestino);
";
            await _dbConnection.ExecuteAsync(
                sqlInsertTemp,
                new
                {
                    IdAreaAux = idAreaAux.Value,
                    IdAreaDestino = idAreaDestino
                }
            ).ConfigureAwait(false);

            // 8) Atualizar chamada: definir destino = área auxiliar
            const string sqlUpdateChamadaDestino = @"
UPDATE chamada
   SET id_areaarmazenagemdestino = @IdAreaAux
 WHERE id_chamada = @IdChamada;
";
            await _dbConnection.ExecuteAsync(
                sqlUpdateChamadaDestino,
                new { IdAreaAux = idAreaAux.Value, IdChamada = idChamada }
            ).ConfigureAwait(false);

            // Sucesso
            return new ValidacaoEnderecoResult
            {
                IsValid = true,
                Mensagem = string.Empty
            };
        }
        finally
        {
            if (_dbConnection.State == ConnectionState.Open)
                _dbConnection.Close();
        }
    }

    public async Task<ValidacaoEnderecoResult> DesalocarEnderecoAsync(Guid idChamada)
    {
        const int STATUS_AREA_LIVRE = 1;

        // 1) Buscar o id_areaarmazenagemleitura na tabela 'chamada'
        const string sqlSelectAreaLeitura = @"
SELECT id_areaarmazenagemleitura
FROM chamada
WHERE id_chamada = @IdChamada;
";
        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();

        try
        {
            var idAreaLeitura = await _dbConnection
                .QuerySingleOrDefaultAsync<long?>(
                    sqlSelectAreaLeitura,
                    new { IdChamada = idChamada }
                )
                .ConfigureAwait(false);

            // 2) Se não houve área de leitura associada à chamada
            if (!idAreaLeitura.HasValue)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Posição não encontrada"
                };
            }

            // 3a) Limpar o pallet que está nessa área de leitura
            const string sqlUpdatePallet = @"
UPDATE pallet
   SET id_areaarmazenagem = NULL
 WHERE id_areaarmazenagem = @IdAreaLeitura;
";
            await _dbConnection.ExecuteAsync(
                sqlUpdatePallet,
                new { IdAreaLeitura = idAreaLeitura.Value }
            ).ConfigureAwait(false);

            // 3b) Marcar a área como livre (fg_status = 1) e limpar id_agrupador
            const string sqlUpdateArea = @"
UPDATE areaarmazenagem
   SET fg_status   = @StatusLivre,
       id_agrupador = NULL
 WHERE id_areaarmazenagem = @IdAreaLeitura;
";
            await _dbConnection.ExecuteAsync(
                sqlUpdateArea,
                new
                {
                    StatusLivre = STATUS_AREA_LIVRE,
                    IdAreaLeitura = idAreaLeitura.Value
                }
            ).ConfigureAwait(false);

            // 4) Retorna sucesso com mensagem vazia
            return new ValidacaoEnderecoResult
            {
                IsValid = true,
                Mensagem = string.Empty
            };
        }
        finally
        {
            if (_dbConnection.State == ConnectionState.Open)
                _dbConnection.Close();
        }
    }

    public async Task<ValidacaoEnderecoResult> LeituraBufferOrdemTransferenciaAsync(Guid idChamada)
    {
        const byte STATUS_AGRUPADOR_RESERVADO = 2;
        const byte STATUS_AGRUPADOR_FECHADO = 4;
        const byte STATUS_CAIXA_ARMAZENADA = 4;
        const byte STATUS_PALLET_CHEIO = 3;
        const string PARAM_ATIVIDADE = "Atividade Para Armazenar Pallet";

        // 1) Buscar dados iniciais da chamada
        const string sqlSelectChamada = @"
SELECT
    id_areaarmazenagemleitura   AS IdAreaLeitura,
    id_areaarmazenagemdestino   AS IdAreaDestino,
    id_palletdestino            AS IdPallet,
    id_operador                 AS IdOperador,
    id_atividade                AS IdAtividade
FROM chamada
WHERE id_chamada = @IdChamada;
";
        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();

        LeituraBufferDto chamadaDados = null;

        try
        {
            chamadaDados = await _dbConnection
                .QuerySingleOrDefaultAsync<LeituraBufferDto>(
                    sqlSelectChamada,
                    new { IdChamada = idChamada }
                )
                .ConfigureAwait(false);

            if (chamadaDados == null)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Chamada não encontrada."
                };
            }

            var idAreaLeitura = chamadaDados.IdAreaLeitura;
            var idAreaDestino = chamadaDados.IdAreaDestino;
            var idPallet = chamadaDados.IdPallet;
            var idOperador = chamadaDados.IdOperador;
            var idAtividade = chamadaDados.IdAtividade;

            // 2) Verificar se endereços de leitura e destino coincidem
            const string sqlSelectEndereco = @"
SELECT id_endereco
FROM areaarmazenagem
WHERE id_areaarmazenagem = @IdArea;
";
            var idEndLeitura = await _dbConnection
                .QuerySingleOrDefaultAsync<int?>(
                    sqlSelectEndereco,
                    new { IdArea = idAreaLeitura }
                )
                .ConfigureAwait(false);

            var idEndDestino = await _dbConnection
                .QuerySingleOrDefaultAsync<int?>(
                    sqlSelectEndereco,
                    new { IdArea = idAreaDestino }
                )
                .ConfigureAwait(false);

            if (!idEndLeitura.HasValue
                || !idEndDestino.HasValue
                || idEndLeitura.Value != idEndDestino.Value)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Você não está no local correto"
                };
            }

            // 3) Registrar histórico em 'historico'
            var nmUsuario = idOperador.HasValue
                ? idOperador.Value.ToString()
                : string.Empty;

            var idRegistro = idPallet.ToString();
            var dsOperacao = "Movimentação Chamada: atividade="
                             + idAtividade
                             + "&areaArmazenagem="
                             + idAreaLeitura
                             + "&chamada="
                             + idChamada;

            const string sqlInsertHistorico = @"
INSERT INTO historico
    (dt_historico, nm_usuario, nm_objeto, id_registro, ds_operacao)
VALUES
    (GETDATE(), @NmUsuario, 'pallet', @IdRegistro, @DsOperacao);
";
            await _dbConnection.ExecuteAsync(
                sqlInsertHistorico,
                new
                {
                    NmUsuario = nmUsuario,
                    IdRegistro = idRegistro,
                    DsOperacao = dsOperacao
                }
            ).ConfigureAwait(false);

            // 4) Transferir pallet: mover para área de leitura e marcar status como cheio
            const string sqlUpdatePallet = @"
UPDATE pallet
   SET id_areaarmazenagem = @IdAreaLeitura,
       fg_status           = @StatusPalletCheio
 WHERE id_pallet = @IdPallet;
";
            await _dbConnection.ExecuteAsync(
                sqlUpdatePallet,
                new
                {
                    IdAreaLeitura = idAreaLeitura,
                    StatusPalletCheio = STATUS_PALLET_CHEIO,
                    IdPallet = idPallet
                }
            ).ConfigureAwait(false);

            // 5) Buscar agrupadores das caixas associadas a este pallet
            const string sqlSelectAgrpadores = @"
SELECT DISTINCT id_agrupador
FROM caixa
WHERE id_pallet = @IdPallet
  AND fg_status = @StatusCaixaArmazenada
  AND id_agrupador IS NOT NULL;
";
            var agrupadores = (await _dbConnection.QueryAsync<Guid>(
                sqlSelectAgrpadores,
                new
                {
                    IdPallet = idPallet,
                    StatusCaixaArmazenada = STATUS_CAIXA_ARMAZENADA
                }
            ).ConfigureAwait(false)).ToList();

            // 6) Reabrir agrupadores (se estiverem fechados)
            if (agrupadores.Any())
            {
                const string sqlUpdateAgrupador = @"
UPDATE agrupadorativo
   SET fg_status = @StatusAgrupadorReservado,
       id_areaarmazenagem = NULL
 WHERE id_agrupador IN @Agrupadores
   AND fg_status = @StatusAgrupadorFechado;
";
                await _dbConnection.ExecuteAsync(
                    sqlUpdateAgrupador,
                    new
                    {
                        Agrupadores = agrupadores,
                        StatusAgrupadorReservado = STATUS_AGRUPADOR_RESERVADO,
                        StatusAgrupadorFechado = STATUS_AGRUPADOR_FECHADO
                    }
                ).ConfigureAwait(false);
            }

            // 7a) Obter o código da atividade “Atividade Para Armazenar Pallet”
            const string sqlSelectParametro = @"
SELECT TOP 1 CAST(ISNULL(nm_valor, '0') AS int)
FROM parametro
WHERE nm_parametro = @ParamAtividade;
";
            var idAtividadeParam = await _dbConnection
                .QuerySingleOrDefaultAsync<int?>(
                    sqlSelectParametro,
                    new { ParamAtividade = PARAM_ATIVIDADE }
                )
                .ConfigureAwait(false) ?? 0;

            // 7b) Executar sp_siag_criachamada para armazenar o pallet novamente
            //     Assuma que o SP sp_siag_criachamada tem cinco parâmetros na ordem:
            //     @id_atividade, @id_pallet, @param3, @id_pallet_destino, @param5.
            //     Ajuste os nomes conforme definição exata do SP.
            var parametrosCriarChamada = new DynamicParameters();
            parametrosCriarChamada.Add("id_atividade", idAtividadeParam, DbType.Int32);
            parametrosCriarChamada.Add("id_pallet", idPallet, DbType.Int32);
            parametrosCriarChamada.Add("param3", null, DbType.Int32);
            parametrosCriarChamada.Add("id_pallet_destino", idPallet, DbType.Int32);
            parametrosCriarChamada.Add("param5", null, DbType.Int32);

            await _dbConnection.ExecuteAsync(
                "sp_siag_criachamada",
                parametrosCriarChamada,
                commandType: CommandType.StoredProcedure
            ).ConfigureAwait(false);

            // 8) Sucesso
            return new ValidacaoEnderecoResult
            {
                IsValid = true,
                Mensagem = string.Empty
            };
        }
        finally
        {
            if (_dbConnection.State == ConnectionState.Open)
                _dbConnection.Close();
        }
    }

    public async Task<ValidacaoEnderecoResult> LeituraBufferAsync(Guid idChamada)
    {
        // Constantes definidas na SP
        const int TIPO_ENDERECO_BUFFER = 4;
        const long ID_AREA_LEITURA1 = 100800000;
        const long ID_AREA_LEITURA2 = 100900000;
        const long ID_AREA_LEITURA3 = 100801000;
        const long ID_AREA_LEITURA4 = 100802000;
        const long ID_AREA_LEITURA5 = 100803000;
        const long ID_AREA_LEITURA6 = 100804000;
        const long ID_AREA_LEITURA7 = 100805000;
        const long ID_AREA_LEITURA8 = 100806000;
        const int STATUS_AREA_LIVRE = 1;
        const string PARAM_STAGEIN = "TIPO AREA STAGEIN";
        const byte STATUS_CAIXA_ARMAZENADA = 4;
        const byte STATUS_PALLET_CHEIO = 3;
        const int RESPONSABILIDADE_DEFAULT = 7508;

        // 1) Buscar dados iniciais da 'chamada'
        const string sqlSelectChamada = @"
SELECT
    id_palletleitura       AS IdPalletLeitura,
    id_areaarmazenagemleitura AS IdAreaLeitura,
    id_operador            AS IdOperador,
    id_atividade           AS IdAtividade
FROM chamada WITH(NOLOCK)
WHERE id_chamada = @IdChamada;
";
        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();

        LeituraBufferDto chamadaDados = null;

        try
        {
            chamadaDados = await _dbConnection.QuerySingleOrDefaultAsync<LeituraBufferDto>(
                sqlSelectChamada,
                new { IdChamada = idChamada }
            ).ConfigureAwait(false);

            if (chamadaDados == null)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Chamada não encontrada."
                };
            }

            var idPalletLeitura = chamadaDados.IdPalletLeitura;
            var idAreaLeitura = chamadaDados.IdAreaLeitura;
            var idOperador = chamadaDados.IdOperador;
            var idAtividade = chamadaDados.IdAtividade;

            // 2) Verificar se a área de leitura está entre as áreas válidas
            var areasValidas = new HashSet<long>
                {
                    ID_AREA_LEITURA1,
                    ID_AREA_LEITURA2,
                    ID_AREA_LEITURA3,
                    ID_AREA_LEITURA4,
                    ID_AREA_LEITURA5,
                    ID_AREA_LEITURA6,
                    ID_AREA_LEITURA7,
                    ID_AREA_LEITURA8
                };

            if (!idAreaLeitura.HasValue || !areasValidas.Contains(idAreaLeitura.Value))
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

            // 4) Executar sp_siag_destinopallet para obter destinos possíveis
            //    Assume-se que sp_siag_destinopallet retorna linhas com colunas (id_endereco INT, id_preenchimento INT)
            var destinos = (await _dbConnection.QueryAsync<DestinoDto>(
                "sp_siag_destinopallet",
                new { id_pallet = idPalletLeitura },
                commandType: CommandType.StoredProcedure
            ).ConfigureAwait(false)).ToList();

            // Extrair lista distinta de id_endereco
            var enderecos = destinos
                .Select(d => d.IdEndereco)
                .Distinct()
                .ToList();

            // 5) Se não houver qualquer destino, log de EPRLB01 e retorna erro
            if (!enderecos.Any())
            {
                await _dbConnection.ExecuteAsync(
                    "dbo.LogIneficienciaTranspaleteira",
                    new { codigo = "EPRLB01" },
                    commandType: CommandType.StoredProcedure
                ).ConfigureAwait(false);

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
                await _dbConnection.ExecuteAsync(
                    "dbo.LogIneficienciaTranspaleteira",
                    new { codigo = "EPRLB02" },
                    commandType: CommandType.StoredProcedure
                ).ConfigureAwait(false);

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

            // Verificar se o pallet está no buffer (endereço tipo 4)
            const string sqlVerificaBuffer = @"
SELECT TOP 1 1
FROM areaarmazenagem WITH(NOLOCK)
INNER JOIN endereco WITH(NOLOCK)
  ON endereco.id_endereco = areaarmazenagem.id_endereco
WHERE id_areaarmazenagem = @IdAreaOrigem
  AND endereco.id_tipoendereco = @TipoEnderecoBuffer;
";
            var estaNoBuffer = await _dbConnection.QuerySingleOrDefaultAsync<int?>(
                sqlVerificaBuffer,
                new
                {
                    IdAreaOrigem = idAreaOrigem,
                    TipoEnderecoBuffer = TIPO_ENDERECO_BUFFER
                }
            ).ConfigureAwait(false);

            if (!estaNoBuffer.HasValue)
            {
                await _dbConnection.ExecuteAsync(
                    "dbo.LogIneficienciaTranspaleteira",
                    new { codigo = $"EPRLB03" },
                    commandType: CommandType.StoredProcedure
                ).ConfigureAwait(false);

                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = $"EPRLB03 - O pallet {idPalletLeitura} não está localizado no buffer"
                };
            }

            // 8) Registrar histórico em 'historicopallet' (silenciando erro)
            try
            {
                const string sqlInsertHistoricoPallet = @"
INSERT INTO historicopallet
    (dt_evento, id_pallet, id_atividade, id_responsabilidade, id_operador, id_chamada, id_areaarmazenagem)
VALUES
    (GETDATE(), @IdPalletLeitura, @IdAtividade, @Responsabilidade, @IdOperador, @IdChamada, @IdAreaLeitura);
";
                await _dbConnection.ExecuteAsync(
                    sqlInsertHistoricoPallet,
                    new
                    {
                        IdPalletLeitura = idPalletLeitura,
                        IdAtividade = idAtividade,
                        Responsabilidade = RESPONSABILIDADE_DEFAULT,
                        IdOperador = idOperador,
                        IdChamada = idChamada,
                        IdAreaLeitura = idAreaLeitura
                    }
                ).ConfigureAwait(false);
            }
            catch
            {
                // “Deu ruim, parou” – ignora qualquer falha aqui
            }

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

            const string sqlInsertHistorico = @"
INSERT INTO historico
    (dt_historico, nm_usuario, nm_objeto, id_registro, ds_operacao)
VALUES
    (GETDATE(), @NmUsuario, 'pallet', @IdRegistro, @DsOperacao);
";
            await _dbConnection.ExecuteAsync(
                sqlInsertHistorico,
                new
                {
                    NmUsuario = nmUsuario,
                    IdRegistro = idPalletLeitura.ToString(),
                    DsOperacao = dsOperacao
                }
            ).ConfigureAwait(false);

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
        finally
        {
            if (_dbConnection.State == ConnectionState.Open)
                _dbConnection.Close();
        }
    }

    public async Task<ValidacaoEnderecoResult> LeituraDocaOrdemInternaAsync(Guid idChamada)
    {
        // Constantes da procedure
        const byte TIPO_AGRUPADOR_SALDO = 2;
        const byte TIPO_AGRUPADOR_EE = 3;
        const byte STATUS_AGRUPADOR_RESERVADO = 2;
        const byte STATUS_AGRUPADOR_FECHADO = 4;
        const byte STATUS_CAIXA_EMBALADA = 3;
        const byte STATUS_CAIXA_ARMAZENADA = 4;
        const byte STATUS_CAIXA_CONSUMIDA = 7;
        const byte STATUS_PALLET_LIVRE = 1;

        // 1) Buscar dados da chamada
        const string sqlSelectChamada = @"
SELECT
    id_areaarmazenagemdestino AS IdAreaDestino,
    id_areaarmazenagemleitura AS IdAreaLeitura,
    id_palletdestino          AS IdPallet,
    id_operador               AS IdOperador,
    id_atividade              AS IdAtividade
FROM chamada
WHERE id_chamada = @IdChamada;
";
        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();

        LeituraDocaDto chamadaDados = null;

        try
        {
            chamadaDados = await _dbConnection
                .QuerySingleOrDefaultAsync<LeituraDocaDto>(
                    sqlSelectChamada,
                    new { IdChamada = idChamada }
                )
                .ConfigureAwait(false);

            if (chamadaDados == null)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Chamada não encontrada."
                };
            }

            var idAreaDestino = chamadaDados.IdAreaDestino;
            var idAreaLeitura = chamadaDados.IdAreaLeitura;
            var idPallet = chamadaDados.IdPallet;
            var idOperador = chamadaDados.IdOperador;
            var idAtividade = chamadaDados.IdAtividade;

            // 2) Verificar se o endereço de leitura e destino coincidem
            const string sqlSelectEndereco = @"
SELECT id_endereco
FROM areaarmazenagem
WHERE id_areaarmazenagem = @IdArea;
";
            var idEndLeitura = await _dbConnection.QuerySingleOrDefaultAsync<int?>(
                sqlSelectEndereco,
                new { IdArea = idAreaLeitura }
            ).ConfigureAwait(false);

            var idEndDestino = await _dbConnection.QuerySingleOrDefaultAsync<int?>(
                sqlSelectEndereco,
                new { IdArea = idAreaDestino }
            ).ConfigureAwait(false);

            if (!idEndLeitura.HasValue
                || !idEndDestino.HasValue
                || idEndLeitura.Value != idEndDestino.Value)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Você não está na doca correta"
                };
            }

            // 3) Registrar histórico em 'historico'
            var nmUsuario = idOperador.HasValue
                ? idOperador.Value.ToString()
                : string.Empty;

            var dsOperacao = "Movimentação Chamada: atividade="
                             + idAtividade
                             + "&areaArmazenagem="
                             + idAreaLeitura
                             + "&chamada="
                             + idChamada;

            const string sqlInsertHistorico = @"
INSERT INTO historico
    (dt_historico, nm_usuario, nm_objeto, id_registro, ds_operacao)
VALUES
    (GETDATE(), @NmUsuario, 'pallet', @IdRegistro, @DsOperacao);
";
            await _dbConnection.ExecuteAsync(
                sqlInsertHistorico,
                new
                {
                    NmUsuario = nmUsuario,
                    IdRegistro = idPallet.ToString(),
                    DsOperacao = dsOperacao
                }
            ).ConfigureAwait(false);

            // 4) ELIMINA VÍNCULO DO PALLET: id_areaarmazenagem=NULL, fg_status=STATUS_PALLET_LIVRE
            const string sqlUpdatePallet = @"
UPDATE pallet
   SET id_areaarmazenagem = NULL,
       fg_status = @StatusPalletLivre
 WHERE id_pallet = @IdPallet;
";
            await _dbConnection.ExecuteAsync(
                sqlUpdatePallet,
                new
                {
                    StatusPalletLivre = STATUS_PALLET_LIVRE,
                    IdPallet = idPallet
                }
            ).ConfigureAwait(false);

            // 5) Buscando agrupadores das caixas associadas ao pallet (status armazenada)
            const string sqlSelectAgrupadores = @"
SELECT DISTINCT
    c.id_agrupador           AS IdAgrupador,
    a.tp_agrupamento         AS TpAgrupamento
FROM caixa AS c
INNER JOIN agrupadorativo AS a
    ON c.id_agrupador = a.id_agrupador
WHERE c.id_pallet = @IdPallet
  AND c.fg_status = @StatusCaixaArmazenada;
";
            var agrupadores = (await _dbConnection.QueryAsync<AgrupadorDto>(
                sqlSelectAgrupadores,
                new
                {
                    IdPallet = idPallet,
                    StatusCaixaArmazenada = STATUS_CAIXA_ARMAZENADA
                }
            ).ConfigureAwait(false)).ToList();

            // Se não houver agrupadores, a lista estará vazia
            if (agrupadores.Any())
            {
                // 6) Atualizar 'caixa' para TP_SALDO ou TP_EE (dt_expedicao = GETDATE())
                var idsSaldoEe = agrupadores
                    .Where(a => a.TpAgrupamento == TIPO_AGRUPADOR_SALDO
                             || a.TpAgrupamento == TIPO_AGRUPADOR_EE)
                    .Select(a => a.IdAgrupador)
                    .ToList();

                if (idsSaldoEe.Any())
                {
                    const string sqlUpdateDtExpedicao = @"
UPDATE caixa
   SET dt_expedicao = GETDATE()
 WHERE id_agrupador IN @IdsAgrupador
   AND id_pallet = @IdPallet;
";
                    await _dbConnection.ExecuteAsync(
                        sqlUpdateDtExpedicao,
                        new
                        {
                            IdsAgrupador = idsSaldoEe,
                            IdPallet = idPallet
                        }
                    ).ConfigureAwait(false);
                }

                // 7) ELIMINA VÍNCULO DAS CAIXAS: id_pallet=NULL e ajustar fg_status
                //    Caso tp_agrupamento em (2, 3) → STATUS_CAIXA_CONSUMIDA; senão → STATUS_CAIXA_EMBALADA
                const string sqlUpdateCaixas = @"
UPDATE caixa
   SET id_pallet = NULL,
       fg_status = CASE
           WHEN a.tp_agrupamento IN (@TipoSaldo, @TipoEE) THEN @StatusCaixaConsumida
           ELSE @StatusCaixaEmbalada
       END
FROM caixa AS c
INNER JOIN agrupadorativo AS a
   ON c.id_agrupador = a.id_agrupador
WHERE c.id_pallet = @IdPallet
  AND c.fg_status = @StatusCaixaArmazenada;
";
                await _dbConnection.ExecuteAsync(
                    sqlUpdateCaixas,
                    new
                    {
                        IdPallet = idPallet,
                        StatusCaixaArmazenada = STATUS_CAIXA_ARMAZENADA,
                        TipoSaldo = TIPO_AGRUPADOR_SALDO,
                        TipoEE = TIPO_AGRUPADOR_EE,
                        StatusCaixaConsumida = STATUS_CAIXA_CONSUMIDA,
                        StatusCaixaEmbalada = STATUS_CAIXA_EMBALADA
                    }
                ).ConfigureAwait(false);

                // 8) Reabrir agrupadores (fg_status=STATUS_AGRUPADOR_RESERVADO, id_areaarmazenagem=NULL)
                var idsAgrupadoresTotais = agrupadores.Select(a => a.IdAgrupador).ToList();

                const string sqlUpdateAgrupador = @"
UPDATE agrupadorativo
   SET fg_status = @StatusReservado,
       id_areaarmazenagem = NULL
 WHERE id_agrupador IN @IdsAgrupador
   AND fg_status = @StatusFechado;
";
                await _dbConnection.ExecuteAsync(
                    sqlUpdateAgrupador,
                    new
                    {
                        IdsAgrupador = idsAgrupadoresTotais,
                        StatusReservado = STATUS_AGRUPADOR_RESERVADO,
                        StatusFechado = STATUS_AGRUPADOR_FECHADO
                    }
                ).ConfigureAwait(false);
            }

            // 9) Sucesso
            return new ValidacaoEnderecoResult
            {
                IsValid = true,
                Mensagem = string.Empty
            };
        }
        finally
        {
            if (_dbConnection.State == ConnectionState.Open)
                _dbConnection.Close();
        }
    }

    public async Task<ValidacaoEnderecoResult> LeituraDocaAsync(Guid idChamada)
    {
        const byte STATUS_AREA_RESERVADO = 2;
        const byte STATUS_AREA_OCUPADO = 3;

        const int STATUS_ORDEM_ALOCADA = 12;
        const int STATUS_ORDEM_CONFERIDA = 13;
        const int STATUS_ORDEM_AGUARDANDO_EXPEDICAO = 21;
        const int STATUS_ORDEM_EXPEDICAO = 22;

        const int RESPONSABILIDADE_DEFAULT = 7508;

        // 1) Buscar dados da chamada
        const string sqlSelectChamada = @"
SELECT
    id_areaarmazenagemdestino AS IdAreaDestino,
    id_areaarmazenagemleitura AS IdAreaLeitura,
    id_palletdestino          AS IdPallet,
    id_operador               AS IdOperador,
    id_atividade              AS IdAtividade
FROM chamada
WHERE id_chamada = @IdChamada;
";
        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();

        LeituraDocaDto chamadaDados = null;
        try
        {
            chamadaDados = await _dbConnection
                .QuerySingleOrDefaultAsync<LeituraDocaDto>(sqlSelectChamada, new { IdChamada = idChamada })
                .ConfigureAwait(false);

            if (chamadaDados == null)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Chamada não encontrada."
                };
            }

            var idAreaDestino = chamadaDados.IdAreaDestino;
            var idAreaLeitura = chamadaDados.IdAreaLeitura;
            var idPallet = chamadaDados.IdPallet;
            var idOperador = chamadaDados.IdOperador;
            var idAtividade = chamadaDados.IdAtividade;

            // 2) Verificar se endereços de leitura e destino coincidem, e obter nr_lado
            const string sqlSelectArea = @"
SELECT id_endereco AS IdEndereco, nr_lado AS NrLado
FROM areaarmazenagem
WHERE id_areaarmazenagem = @IdArea;
";

            var areaLeitura = await _dbConnection
                .QuerySingleOrDefaultAsync<AreaLadoDto>(
                    sqlSelectArea,
                    new { IdArea = idAreaLeitura }
                ).ConfigureAwait(false);

            var areaDestino = await _dbConnection
                .QuerySingleOrDefaultAsync<AreaLadoDto>(
                    sqlSelectArea,
                    new { IdArea = idAreaDestino }
                ).ConfigureAwait(false);

            if (areaLeitura == null || areaDestino == null ||
                areaLeitura.IdEndereco != areaDestino.IdEndereco)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Você não está na doca correta"
                };
            }

            // 3) Verificar se lados coincidem
            if (areaLeitura.NrLado != areaDestino.NrLado)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Você está no lado errado da doca"
                };
            }

            var idEndereco = areaDestino.IdEndereco;
            var nrLado = areaDestino.NrLado;

            // 4) Selecionar a primeira área reservada disponível naquele endereço e lado
            const string sqlSelectAreaReservada = @"
SELECT TOP 1 id_areaarmazenagem
FROM areaarmazenagem
WHERE id_endereco = @IdEndereco
  AND fg_status = @StatusReservado
  AND nr_lado = @NrLado;
";
            var idAreaReservada = await _dbConnection
                .QuerySingleOrDefaultAsync<long?>(
                    sqlSelectAreaReservada,
                    new
                    {
                        IdEndereco = idEndereco,
                        StatusReservado = STATUS_AREA_RESERVADO,
                        NrLado = nrLado
                    }
                ).ConfigureAwait(false);

            if (!idAreaReservada.HasValue)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Nenhuma posição disponível neste lado"
                };
            }

            // 5) Atualizar pallet para apontar à área reservada
            const string sqlUpdatePallet = @"
UPDATE pallet
   SET id_areaarmazenagem = @IdAreaReservada
 WHERE id_pallet = @IdPallet;
";
            await _dbConnection.ExecuteAsync(
                sqlUpdatePallet,
                new
                {
                    IdAreaReservada = idAreaReservada.Value,
                    IdPallet = idPallet
                }
            ).ConfigureAwait(false);

            // 6) Registrar histórico em 'historicopallet' (silenciando erro)
            try
            {
                const string sqlInsertHistoricoPallet = @"
INSERT INTO historicopallet
    (dt_evento, id_pallet, id_atividade, id_responsabilidade, id_operador, id_chamada, id_areaarmazenagem)
VALUES
    (GETDATE(), @IdPallet, @IdAtividade, @Responsabilidade, @IdOperador, @IdChamada, @IdAreaReservada);
";
                await _dbConnection.ExecuteAsync(
                    sqlInsertHistoricoPallet,
                    new
                    {
                        IdPallet = idPallet,
                        IdAtividade = idAtividade,
                        Responsabilidade = RESPONSABILIDADE_DEFAULT,
                        IdOperador = idOperador,
                        IdChamada = idChamada,
                        IdAreaReservada = idAreaReservada.Value
                    }
                ).ConfigureAwait(false);
            }
            catch
            {
                // “Deu ruim mesmo” – ignora qualquer falha aqui
            }

            // 7) Registrar em 'historico'
            var nmUsuario = idOperador.HasValue
                ? idOperador.Value.ToString()
                : string.Empty;

            var dsOperacao = "Movimentação Chamada: atividade="
                             + idAtividade
                             + "&areaArmazenagem="
                             + idAreaReservada.Value
                             + "&chamada="
                             + idChamada;

            const string sqlInsertHistorico = @"
INSERT INTO historico
    (dt_historico, nm_usuario, nm_objeto, id_registro, ds_operacao)
VALUES
    (GETDATE(), @NmUsuario, 'pallet', @IdRegistro, @DsOperacao);
";
            await _dbConnection.ExecuteAsync(
                sqlInsertHistorico,
                new
                {
                    NmUsuario = nmUsuario,
                    IdRegistro = idPallet.ToString(),
                    DsOperacao = dsOperacao
                }
            ).ConfigureAwait(false);

            // 8) Atualizar a área reservada para status ocupado
            const string sqlUpdateArea = @"
UPDATE areaarmazenagem
   SET fg_status = @StatusOcupado
 WHERE id_areaarmazenagem = @IdAreaReservada;
";
            await _dbConnection.ExecuteAsync(
                sqlUpdateArea,
                new
                {
                    StatusOcupado = STATUS_AREA_OCUPADO,
                    IdAreaReservada = idAreaReservada.Value
                }
            ).ConfigureAwait(false);

            return new ValidacaoEnderecoResult
            {
                IsValid = true,
                Mensagem = string.Empty
            };
        }
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
        const int RESPONSABILIDADE_DEFAULT = 7508;

        // 1) Buscar dados da chamada
        const string sqlSelectChamada = @"
SELECT
    id_areaarmazenagemorigem   AS IdAreaOrigem,
    id_areaarmazenagemdestino  AS IdAreaDestino,
    id_areaarmazenagemleitura  AS IdAreaLeitura,
    id_palletorigem            AS IdPallet,
    id_operador                AS IdOperador,
    id_atividade               AS IdAtividade
FROM chamada
WHERE id_chamada = @IdChamada;
";
        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();

        LeituraStageInDto chamadaDados = null;

        try
        {
            chamadaDados = await _dbConnection
                .QuerySingleOrDefaultAsync<LeituraStageInDto>(
                    sqlSelectChamada,
                    new { IdChamada = idChamada }
                )
                .ConfigureAwait(false);

            if (chamadaDados == null)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Chamada não encontrada."
                };
            }

            var idAreaOrigem = chamadaDados.IdAreaOrigem;
            var idAreaLeitura = chamadaDados.IdAreaLeitura;
            var idPallet = chamadaDados.IdPallet;
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

            // 4a) Inserir em historicopallet (silenciando erro)
            try
            {
                const string sqlInsertHistoricoPallet = @"
INSERT INTO historicopallet
    (dt_evento, id_pallet, id_atividade, id_responsabilidade, id_operador, id_chamada, id_areaarmazenagem)
VALUES
    (GETDATE(), @IdPallet, @IdAtividade, @Responsabilidade, @IdOperador, @IdChamada, @IdAreaOrigem);
";
                await _dbConnection.ExecuteAsync(
                    sqlInsertHistoricoPallet,
                    new
                    {
                        IdPallet = idPallet,
                        IdAtividade = idAtividade,
                        Responsabilidade = RESPONSABILIDADE_DEFAULT,
                        IdOperador = idOperador,
                        IdChamada = idChamada,
                        IdAreaOrigem = idAreaOrigem
                    }
                ).ConfigureAwait(false);
            }
            catch
            {
                // “Deu ruim mesmo” – ignora qualquer falha aqui
            }

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

            const string sqlInsertHistorico = @"
INSERT INTO historico
    (dt_historico, nm_usuario, nm_objeto, id_registro, ds_operacao)
VALUES
    (GETDATE(), @NmUsuario, 'pallet', @IdRegistro, @DsOperacao);
";
            await _dbConnection.ExecuteAsync(
                sqlInsertHistorico,
                new
                {
                    NmUsuario = nmUsuario,
                    IdRegistro = idPallet.ToString(),
                    DsOperacao = dsOperacao
                }
            ).ConfigureAwait(false);

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
        finally
        {
            if (_dbConnection.State == ConnectionState.Open)
                _dbConnection.Close();
        }
    }

    public Task<ValidacaoEnderecoResult> SempreOkAsync(Guid idChamada)
    {
        return Task.FromResult(new ValidacaoEnderecoResult
        {
            IsValid = true,
            Mensagem = string.Empty
        });
    }

    public async Task<ValidacaoEnderecoResult> ValidarEnderecoExatoOrigemAsync(Guid idChamada)
    {
        const string sql = @"
SELECT 
    id_areaarmazenagemorigem   AS IdAreaArmazenagemOrigem,
    id_areaarmazenagemleitura  AS IdAreaArmazenagemLeitura
FROM chamada
WHERE id_chamada = @IdChamada;
";

        // Parâmetro nomeado (evita SQL Injection e torna o código mais legível)
        var parametros = new { IdChamada = idChamada };

        // Se a conexão não estiver aberta, abra-a
        if (_dbConnection.State != ConnectionState.Open)
        {
            _dbConnection.Open();
        }

        try
        {
            // Busca os dois campos de uma só vez
            var resultado = await _dbConnection
                .QuerySingleOrDefaultAsync<EnderecoLeituraDto>(sql, parametros)
                .ConfigureAwait(false);

            // Se não encontrou registro, considere falha
            if (resultado == null)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Chamada não encontrada."
                };
            }

            // Lógica idêntica à da procedure:
            //    IF ((leitura != NULL) AND (origem == leitura)) -> sucesso
            //    ELSE -> falha e mensagem "Você não está na posição correta"
            if (resultado.IdAreaArmazenagemLeitura.HasValue
                && resultado.IdAreaArmazenagemOrigem == resultado.IdAreaArmazenagemLeitura.Value)
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = true,
                    Mensagem = string.Empty
                };
            }
            else
            {
                return new ValidacaoEnderecoResult
                {
                    IsValid = false,
                    Mensagem = "Você não está na posição correta"
                };
            }
        }
        finally
        {
            // Fecha a conexão para não manter conexões abertas desnecessariamente
            if (_dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
            }
        }
    }

    public async Task<ValidacaoEnderecoResult> VerificaDesalocaPalletOrigemAsync(Guid idChamada)
    {
        const int RESPONSABILIDADE_DEFAULT = 7508;

        // 1) Buscar dados iniciais da chamada
        const string sqlSelectChamada = @"
SELECT
    id_palletorigem              AS IdPalletOrigem,
    id_palletleitura             AS IdPalletLeitura,
    id_operador                  AS IdOperador,
    id_atividade                 AS IdAtividade,
    id_areaarmazenagemorigem     AS IdAreaArmazenagemOrigem
FROM chamada
WHERE id_chamada = @IdChamada;
";
        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();

        VerificaDto chamadaDados = null;

        try
        {
            chamadaDados = await _dbConnection
                .QuerySingleOrDefaultAsync<VerificaDto>(
                    sqlSelectChamada,
                    new { IdChamada = idChamada }
                )
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

                // 2b) Inserir em historicopallet (silenciando erro)
                try
                {
                    const string sqlInsertHistoricoPallet = @"
INSERT INTO historicopallet
    (dt_evento, id_pallet, id_atividade, id_responsabilidade, id_operador, id_chamada, id_areaarmazenagem)
VALUES
    (GETDATE(), @IdPalletOrigem, @IdAtividade, @Responsabilidade, @IdOperador, @IdChamada, @IdAreaOrigem);
";
                    await _dbConnection.ExecuteAsync(
                        sqlInsertHistoricoPallet,
                        new
                        {
                            IdPalletOrigem = idPalletOrigem,
                            IdAtividade = idAtividade,
                            Responsabilidade = RESPONSABILIDADE_DEFAULT,
                            IdOperador = idOperador,
                            IdChamada = idChamada,
                            IdAreaOrigem = idAreaOrigem
                        }
                    ).ConfigureAwait(false);
                }
                catch
                {
                    // “Deu ruim” – ignora qualquer falha aqui
                }

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

                const string sqlInsertHistorico = @"
INSERT INTO historico
    (dt_historico, nm_usuario, nm_objeto, id_registro, ds_operacao)
VALUES
    (GETDATE(), @NmUsuario, 'pallet', @IdRegistro, @DsOperacao);
";
                await _dbConnection.ExecuteAsync(
                    sqlInsertHistorico,
                    new
                    {
                        NmUsuario = nmUsuario,
                        IdRegistro = idRegistro,
                        DsOperacao = dsOperacao
                    }
                ).ConfigureAwait(false);

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
        finally
        {
            if (_dbConnection.State == ConnectionState.Open)
                _dbConnection.Close();
        }
    }
}