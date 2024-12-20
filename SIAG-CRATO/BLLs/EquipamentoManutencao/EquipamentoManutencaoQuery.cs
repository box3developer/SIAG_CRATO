namespace SIAG_CRATO.BLLs.EquipamentoManutencao
{
    public class EquipamentoManutencaoQuery
    {
        public const string SELECT = @"SELECT 
                                   id_equipamento_manutencao
                                  ,id_equipamento
                                  ,fg_tipo_manutencao
                                  ,dt_inicio
                                  ,dt_fim
                              FROM equipamentomanutencao";

        public const string UPDATE_DTFIM_BY_EQUIPAMENTO = @"UPDATE equipamentomanutencao
		                                                    SET dt_fim = getdate()
		                                                    where id_equipamento = @id_equipamento
		                                                    and dt_fim is null";
    }
}
