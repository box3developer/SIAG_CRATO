namespace SIAG.Application.Armazenagem.Cadastro.DTOs;

public class EquipamentoManutencaoDTO
{
    public int IdEquipamentoManutencao { get; set; }

    public int IdEquipamento { get; set; }

    public EquipamentoDTO? Equipamento { get; set; }

    public int FgTipoManutencao { get; set; }

    public int DtInicio { get; set; }

    public int DtFim { get; set; }
}
