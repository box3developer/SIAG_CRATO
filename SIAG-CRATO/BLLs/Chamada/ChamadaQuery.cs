namespace SIAG_CRATO.BLLs.Chamada;

public class ChamadaQuery
{
    public const string SELECT = @"SELECT 
                                        c.id_chamada, 
                                        c.id_palletorigem, 
                                        c.id_areaarmazenagemorigem, 
                                        c.id_palletdestino, 
                                        c.id_areaarmazenagemdestino, 
                                        c.id_palletleitura, 
                                        c.id_areaarmazenagemleitura, 
                                        c.id_operador, 
                                        c.id_equipamento, 
                                        c.id_atividaderejeicao, 
                                        c.id_atividade, 
                                        c.fg_status, 
                                        c.dt_chamada, 
                                        c.dt_recebida, 
                                        c.dt_atendida, 
                                        c.dt_finalizada, 
                                        c.dt_rejeitada, 
                                        c.id_chamadasuspensa
                                   FROM chamada c
                                   INNER JOIN atividade a WITH(NOLOCK) ON (a.id_atividade = c.id_atividade)";

    public const string SELECT_DISPONIVEL = @"SELECT chamada.id_chamada AS id_chamada,
	                                                 chamada.dt_chamada AS dt_chamada,
	                                                 chamada.id_atividade AS id_atividade,
	                                                 ao.id_endereco AS id_endereco_origem,
	                                                 ad.id_endereco AS id_endereco_destino,
	                                                 0 AS qt_prioridade,
	                                                 0 AS fg_processado,
	                                                 chamada.id_areaarmazenagemorigem AS id_areaarmazenagemorigem,
	                                                 chamada.priorizar AS priorizar
                                              FROM chamada WITH(NOLOCK)
                                                   INNER JOIN atividade WITH(NOLOCK) ON (atividade.id_atividade = chamada.id_atividade)
                                                   LEFT JOIN areaarmazenagem ao ON ao.id_areaarmazenagem = chamada.id_areaarmazenagemorigem
	                                               LEFT JOIN areaarmazenagem ad ON ad.id_areaarmazenagem = chamada.id_areaarmazenagemdestino";

    public const string INSERT = @"INSERT INTO chamada 
                                        (id_chamada, id_palletorigem, id_areaarmazenagemorigem, id_palletdestino, id_areaarmazenagemdestino, id_atividade, fg_status, dt_chamada, priorizar) 
		                           VALUES 
                                        (@idChamada, @idPalletOrigem, @idAreaArmazenagemOrigem, @idPalletDestino, @idAreaArmazenagemdestino, @idAtividade, @statusChamada, GETDATE(), @priorizar)";

    public const string UPDATE = @"UPDATE chamada
                                   SET fg_status = @status, id_equipamento = @idEquipamento, dt_recebida = @dataRecebida, dt_atendida = @dataAtendida, dt_finalizada = @dataFinalizada
                                   WHERE id_chamada = @id_chamada";

    public const string UPDATE_CHAMADA_ORIGEM = @"UPDATE chamada SET id_chamadaorigem = @idChamadaOrigem WHERE id_chamada = @idChamada";
    public const string UPDATE_STATUS = @"UPDATE chamada SET fg_status = @status WHERE id_chamada = @idChamada";
    public const string UPDATE_FINALIZAR = @"UPDATE chamada SET fg_status = @status, dt_finalizada = GETDATE() WHERE id_chamada = @idChamada";
    public const string UPDATE_REJEITAR = @"UPDATE chamada
		                                    SET fg_status = @statusRejeicao, dt_rejeitada = GETDATE(), id_atividaderejeicao = @idAtividadeRejeicao
		                                    WHERE id_chamada = @idChamada";

    public const string UPDATE_ATRIBUIR = @"UPDATE chamada
		                                    SET fg_status = @status, id_operador = @idOperador, id_equipamento = @idEquipamento, dt_recebida = GETDATE()
		                                    WHERE id_chamada = @idChamada";
}
