using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.Application.Armazenagem.Core.DTOs;

public class EquipamentoCheckListOperadorDTO
{
    public int IdEquipamentoCheckListOperador { get; set; }

    public int IdEquipamento { get; set; }
    public EquipamentoDTO? Equipamento { get; set; }

    public long IdOperador { get; set; }
    public OperadorDTO? Operador { get; set; }


    public int IdEquipamentoChecklist { get; set; }
    public EquipamentoChecklistDTO? EquipamentoChecklist { get; set; }

    public bool FgResposta { get; set; }

    public DateTime? DtChecklist { get; set; }
}
