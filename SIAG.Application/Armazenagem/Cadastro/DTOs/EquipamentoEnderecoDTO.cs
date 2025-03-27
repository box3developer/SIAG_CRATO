namespace SIAG.Application.Armazenagem.Cadastro.DTOs;

public class EquipamentoEnderecoDTO
{
    public long IdEquipamentoEndereco { get; set; }

    public int IdEquipamento { get; set; }
    
    public EquipamentoDTO? Equipamento { get; set; }

    public long IdEndereco { get; set; }
}
