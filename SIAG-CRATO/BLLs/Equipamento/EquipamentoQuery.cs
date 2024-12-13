namespace SIAG_CRATO.BLLs.Equipamento;

public class EquipamentoQuery
{
    public const string SELECT = "SELECT id_equipamento, nm_equipamento, id_equipamentomodelo, nm_identificador, id_operador, cd_ultimaleitura, dt_ultimaleituraFROM equipamento WITH(NOLOCK)";
}
