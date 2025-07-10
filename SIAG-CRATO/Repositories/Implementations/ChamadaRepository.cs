using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.Repositories.Interfaces;
using SIAG_CRATO.Services;

namespace SIAG_CRATO.Repositories.Implementations
{
    public class ChamadaRepository : IChamadaRepository
    {
        public async Task<Guid> CriarChamadaAsync(CriarChamadaDTO dto)
        {
            const int STATUS_CHAMADA_INCOMPLETA = 0;
            const int STATUS_CHAMADA_NAO_ATRIBUIDA = 1;

            var novaChamadaId = Guid.NewGuid();

            // 1) Cria e abre a conexão localmente, conforme seu padrão “using var conexao = new SqlConnection(...)”
            using var connection = new SqlConnection(Global.Conexao);
            await connection.OpenAsync();

            // 2) Inicia a transação
            using var transaction = connection.BeginTransaction();
            try
            {
                // 3) Inserir registro na tabela 'chamada'
                const string insertChamadaSql = @"
INSERT INTO chamada
    (id_chamada, id_palletorigem, id_areaarmazenagemorigem,
     id_palletdestino, id_areaarmazenagemdestino, id_atividade,
     fg_status, dt_chamada, priorizar, id_operador, id_equipamento)
VALUES
    (@ChamadaId, @PalletOrigem, @AreaOrigem,
     @PalletDestino, @AreaDestino, @Atividade,
     @StatusIncompleta, GETDATE(), @Priorizar, @IdOperador, @IdEquipamento);";

                var insertParams = new
                {
                    ChamadaId = novaChamadaId,
                    PalletOrigem = dto.IdPalletOrigem,
                    AreaOrigem = dto.IdAreaArmazenagemOrigem,
                    PalletDestino = dto.IdPalletDestino,
                    AreaDestino = dto.IdAreaArmazenagemDestino,
                    Atividade = dto.IdAtividade,
                    StatusIncompleta = STATUS_CHAMADA_INCOMPLETA,
                    Priorizar = dto.Priorizar,
                    IdOperador = dto.IdOperador,
                    IdEquipamento = dto.IdEquipamento
                };

                await connection.ExecuteAsync(
                    insertChamadaSql,
                    insertParams,
                    transaction
                );

                // 4) Recuperar tarefas associadas à atividade
                const string selectTarefasSql = @"
SELECT
    id_tarefa   AS IdTarefa,
    cd_sequencia AS Sequencia
FROM atividadetarefa WITH(NOLOCK)
WHERE id_atividade = @Atividade
ORDER BY cd_sequencia;";

                var tarefas = await connection.QueryAsync<Tarefa>(
                    selectTarefasSql,
                    new { Atividade = dto.IdAtividade },
                    transaction
                );

                // 5) Inserir em 'chamadatarefa' para cada tarefa
                const string insertChamadaTarefaSql = @"
INSERT INTO chamadatarefa
    (id_chamada, id_tarefa)
VALUES
    (@ChamadaId, @TarefaId);";

                foreach (var tarefa in tarefas)
                {
                    await connection.ExecuteAsync(
                        insertChamadaTarefaSql,
                        new { ChamadaId = novaChamadaId, TarefaId = tarefa.IdTarefa },
                        transaction
                    );
                }

                // 6) Atualizar status da chamada para "não atribuída"
                const string updateChamadaSql = @"
UPDATE chamada
SET fg_status = @StatusNaoAtribuida
WHERE id_chamada = @ChamadaId;";

                await connection.ExecuteAsync(
                    updateChamadaSql,
                    new { ChamadaId = novaChamadaId, StatusNaoAtribuida = STATUS_CHAMADA_NAO_ATRIBUIDA },
                    transaction
                );

                // 7) Commit da transação
                transaction.Commit();
                return novaChamadaId;
            }
            catch
            {
                // 8) Em caso de erro, faz rollback e propaga
                transaction.Rollback();
                throw;
            }
        }

        public async Task<bool> LivraPallet(int IdPallet, long IdAreaArmazenagem)
        {
            var sqlPallet = @"UPDATE pallet
                   SET fg_status = 1,
                       id_agrupador = NULL
                 WHERE id_pallet = @IdPallet
            ";

            var sqlCaixa = @"UPDATE caixa
                SET id_pallet = NULL
                WHERE id_pallet = @IdPallet
            ";

            var sqlArea = @"UPDATE areaarmazenagem
                SET id_agrupador_reservado = NULL
            ";

            using (var conexao = new SqlConnection(Global.Conexao))
            {
                await conexao.ExecuteAsync(sqlPallet, new { IdPallet });
                await conexao.ExecuteAsync(sqlCaixa, new { IdPallet });
                await conexao.ExecuteAsync(sqlArea, new { IdAreaArmazenagem });
            }

            return true;
        }

        private class Tarefa
        {
            public int IdTarefa { get; set; }
            public int Sequencia { get; set; }
        }
    }
}
