namespace SIAG_CRATO.BLLs.Operador;

public class OperadorQuery
{
    public const string SELECT = @"SELECT id_operador, nm_operador, dt_login, nm_cpf, nr_localidade, nm_nfcoperador, fg_funcao, id_responsavel FROM operador WITH(NOLOCK)";
    public const string SELECT_PARAMETRO = @"SELECT nm_valor FROM parametro WITH(NOLOCK)";
}
