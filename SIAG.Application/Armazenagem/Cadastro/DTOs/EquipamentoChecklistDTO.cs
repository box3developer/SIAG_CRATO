namespace SIAG.Application.Armazenagem.Cadastro.DTOs;

public class EquipamentoChecklistDTO
{
    public int IdEquipamentoChecklist { get; set; }

    public int IdEquipamentoModelo { get; set; }

    public EquipamentoModeloDTO? EquipamentoModelo { get; set; }

    public string? NmDescricao { get; set; }

    public bool? FgCritico { get; set; }

    public int? FgStatus { get; set; }
}
