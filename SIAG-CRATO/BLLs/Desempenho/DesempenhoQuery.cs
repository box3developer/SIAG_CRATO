namespace SIAG_CRATO.BLLs.Desempenho;

public class DesempenhoQuery
{
    public const string SELECT = "SELECT id_operador, id_setortrabalho, id_equipamentomodelo, nr_temporealizado, qt_realizada FROM desempenhoonline WITH(NOLOCK)";
}
