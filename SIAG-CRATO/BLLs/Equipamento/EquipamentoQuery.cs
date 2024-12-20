namespace SIAG_CRATO.BLLs.Equipamento;

public class EquipamentoQuery
{
    public const string SELECT = "SELECT id_equipamento, nm_equipamento, id_equipamentomodelo, nm_identificador, id_operador, cd_ultimaleitura, dt_ultimaleituraFROM equipamento WITH(NOLOCK)";
    public const string UPDATE_LEITURA = @"UPDATE equipamento SET cd_ultimaleitura = NULL, dt_ultimaleitura = NULL WHERE id_equipamento = @idEquipamento";
    public const string UPDATE_PENDENTE = @"UPDATE equipamento SET cd_leitura_pendente = @idCaixa WHERE id_equipamento = @idEquipamento";
    public const string UPDATE_DATE = @"UPDATE equipamento SET dt_ultimaleitura = GETDATE()";
    public const string UPDATE_ENDERECO = @"UPDATE equipamento SET dt_ultimaleitura = GETDATE(), id_endereco = @id_endereco";
    public const string UPDATE_EQUIPAMENTO_OPERADOR = @"UPDATE equipamento SET id_operador = @id_operador, fg_status = 1 WHERE id_equipamento = @id_equipamento";
    public const string UPDATE_EQUIPAMENTO_OPERADOR_LOGOFF = @"UPDATE equipamento SET id_operador = null WHERE id_equipamento = @id_equipamento";
}
