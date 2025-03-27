namespace SIAG.Application.Chamada.Cadastro.DTOs;

public class AtividadeRotinaDTO
{
    public int IdAtividadeRotina { get; set; }

    public string NmAtividadeRotina { get; set; } = string.Empty;

    public string NmProcedure { get; set; } = string.Empty;

    public int FgTipo { get; set; }
}
