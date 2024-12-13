namespace SIAG_CRATO.BLLs.Chamada;

public class ChamadaQuery
{
    public const string SELECT = @"SELECT 
                                        id_chamada, id_palletorigem, id_areaarmazenagemorigem, id_palletdestino, id_areaarmazenagemdestino, 
                                        id_palletleitura, id_areaarmazenagemleitura, id_operador, id_equipamento, id_atividaderejeicao, 
                                        id_atividade, fg_status, dt_chamada, dt_recebida, dt_atendida, dt_finalizada, dt_rejeitada, id_chamadasuspensa
                                   FROM chamada";


}
