namespace SIAG_CRATO.BLLs.AgrupadorAtivo
{
    public class AgrupadorAtivoQuery
    {
        public static string SELECT = $@"SELECT * FROM agrupadoratico";

        public static string UPDATE_FINALIZA_AGRUPADOR = @"UPDATE agrupadorativo SET fg_status = 4 WHERE id_agrupador = @idAgrupador";

        public static string UPDATE_LIBERA_AGRUPADOR = @"UPDATE agrupadorativo SET id_areaarmazenagem = NULL WHERE id_agrupador = @idAgrupador";
    }
}
