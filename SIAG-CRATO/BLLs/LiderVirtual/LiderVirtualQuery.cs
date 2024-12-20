namespace SIAG_CRATO.BLLs.LiderVirtual;

public class LiderVirtualQuery
{
    public const string SELECT = @"SELECT id_lidervirtual, id_operador, id_equipamentoorigem, id_equipamentodestino, dt_login, dt_logoff, id_operadorlogin, dt_loginlimite FROM lidervirtual WITH(NOLOCK)";
    public const string INSERT = @"INSERT INTO lidervirtual (id_operador, id_equipamentoorigem, id_equipamentodestino, dt_login, dt_logoff, id_operadorlogin, dt_loginlimite)
                                   VALUES (@IdOperador, @IdEquipamentoOrigem, @IdEquipamentoDestino, @DataLogin, @DataLogoff, @IdOperadorLogin, @DataLoginLimite)";

    public const string UPDATE = @"UPDATE lidervirtual
                                   SET
                                       id_operador = @IdOperador,
                                       id_equipamentoorigem = @IdEquipamentoOrigem,
                                       id_equipamentodestino = @IdEquipamentoDestino,
                                       dt_login = @DataLogin,
                                       dt_logoff = @DataLogoff,
                                       id_operadorlogin = @IdOperadorLogin,
                                       dt_loginlimite = @DataLoginLimite
                                   WHERE id_lidervirtual = @IdLiderVirtual";
}
