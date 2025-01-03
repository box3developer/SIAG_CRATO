namespace SIAG_CRATO.BLLs.LiderVirtual;

public class LiderVirtualQuery
{
    public const string SELECT = @"SELECT id_lidervirtual, id_operador, id_equipamentoorigem, id_equipamentodestino, dt_login, dt_logoff, id_operadorlogin, dt_loginlimite FROM lidervirtual WITH(NOLOCK)";
    public const string INSERT = @"INSERT INTO lidervirtual (id_operador, id_equipamentoorigem, id_equipamentodestino, dt_login, dt_logoff, id_operadorlogin, dt_loginlimite)
                                   VALUES (@idOperador, @idEquipamentoOrigem, @idEquipamentoDestino, @dataLogin, @dataLogoff, @idOperadorLogin, @dataLoginLimite)";

    public const string UPDATE = @"UPDATE lidervirtual
                                   SET
                                       id_operador = @idOperador,
                                       id_equipamentoorigem = @idEquipamentoOrigem,
                                       id_equipamentodestino = @idEquipamentoDestino,
                                       dt_login = @dataLogin,
                                       dt_logoff = @dataLogoff,
                                       id_operadorlogin = @idOperadorLogin,
                                       dt_loginlimite = @dataLoginLimite
                                   WHERE id_lidervirtual = @idLiderVirtual";
}
