namespace SIAG_CRATO.BLLs.EquipamentoCheckListOperador;

public class EquipamentoCheckListOperadorQuery
{
    public const string INSERT = "INSERT INTO equipamentochecklistoperador (id_equipamento, id_operador, id_equipamentochecklist, fg_resposta, dt_checklist) VALUES (@idEquipamento, @idOperador, @idChecklist, @resposta, @data)";
}
