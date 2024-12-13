namespace SIAG_CRATO.BLLs.Operador;

public class OperadorQuery
{
    public const string SELECT = @"SELECT id_operador, nm_operador, nm_cpf, fg_funcao FROM operador WITH(NOLOCK)";
    public const string SELECT_PARAMETRO = @"SELECT nm_valor FROM parametro WITH(NOLOCK)";
}
