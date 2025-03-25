namespace SIAG_CRATO.BLLs.Equipamento;

public class EquipamentoQuery
{
    public const string SELECT = @"SELECT id_equipamento, id_equipamentomodelo, id_setortrabalho, id_operador, nm_equipamento, 
                                          nm_abreviado_equipamento, nm_identificador, fg_status, dt_inclusao, dt_manutencao, 
                                          dt_ultimaleitura, id_endereco, nm_ip, fg_statustrocacaracol, cd_leitura_pendente, cd_ultimaleitura, nm_observacao
                                   FROM equipamento WITH(NOLOCK)";

    public const string UPDATE = @"UPDATE equipamento 
                                   SET id_setortrabalho = @setor, nm_equipamento = @descricao, 
                                       id_equipamentomodelo = @modelo, fg_status = @status, 
                                       dt_inclusao = @dataInclusao, dt_manutencao = @dataManutencao, 
                                       nm_identificador = @identificador, nm_ip = @ip, 
                                       nm_abreviado_equipamento = @descricaoAbreviada 
                                   WHERE id_equipamento = @idEquipamento";

    public const string UPDATE_LEITURA = @"UPDATE equipamento SET cd_ultimaleitura = NULL, dt_ultimaleitura = NULL WHERE id_equipamento = @idEquipamento";
    public const string UPDATE_PENDENTE = @"UPDATE equipamento SET cd_leitura_pendente = @idCaixa WHERE id_equipamento = @idEquipamento";
    public const string UPDATE_DATE = @"UPDATE equipamento SET dt_ultimaleitura = GETDATE() WHERE id_equipamento = @id_equipamento";
    public const string UPDATE_ENDERECO = @"UPDATE equipamento SET dt_ultimaleitura = GETDATE(), id_endereco = @id_endereco WHERE id_equipamento = @id_equipamento";
    public const string UPDATE_EQUIPAMENTO_OPERADOR = @"UPDATE equipamento SET id_operador = @id_operador, fg_status = 1 WHERE id_equipamento = @id_equipamento";
    public const string UPDATE_EQUIPAMENTO_OPERADOR_LOGOFF = @"UPDATE equipamento SET id_operador = null WHERE id_equipamento = @id_equipamento";
    public const string UPDATE_NOVA_LEITURA = @"UPDATE equipamento SET cd_ultimaleitura = @id_caixa, dt_ultimaleitura = @dt_ultimaleitura WHERE id_equipamento = @id_equipamento";
}
