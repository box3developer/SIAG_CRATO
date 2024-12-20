namespace SIAG_CRATO.BLLs.OperadorHistorico
{
    public class OperadorHistoricoQuery
    {
        public const string INSERT = @"INSERT INTO operadorhistorico 
                                        (id_operador,id_equipamento,cd_evento,dt_evento) 
		                                VALUES (@id_operador,@id_equipamento,@evento ,getdate())
";
    }
}
