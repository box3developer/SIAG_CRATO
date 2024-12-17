namespace SIAG_CRATO.BLLs.Equipamento;

public class EquipamentoQuery
{
    public const string SELECT = "SELECT id_equipamento, nm_equipamento, id_equipamentomodelo, nm_identificador, id_operador, cd_ultimaleitura, dt_ultimaleituraFROM equipamento WITH(NOLOCK)";
    public const string UPDATE_LEITURA = @"UPDATE equipamento SET cd_ultimaleitura = NULL, dt_ultimaleitura = NULL WHERE id_equipamento = @idEquipamento";
    public const string UPDATE_PENDENTE = @"UPDATE equipamento SET cd_leitura_pendente = @idCaixa WHERE id_equipamento = @idEquipamento";
}
