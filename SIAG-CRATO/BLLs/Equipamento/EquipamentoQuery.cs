namespace SIAG_CRATO.BLLs.Equipamento;

public class EquipamentoQuery
{
    public const string SELECT = "SELECT id_equipamento, nm_equipamento, id_equipamentomodelo, nm_identificador, id_operador, cd_ultimaleitura, dt_ultimaleituraFROM equipamento WITH(NOLOCK)";

    public const string UPDATE_ENDERECO = @"UPDATE equipamento
                                           SET 
                                              dt_ultimaleitura = GETDATE(),
                                              id_endereco = @id_endereco";


    public const string UPDATE_DATE = @"UPDATE equipamento
                                                SET 
                                                    dt_ultimaleitura = GETDATE()";
      

}
