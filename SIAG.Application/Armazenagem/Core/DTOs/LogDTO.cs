namespace SIAG.Application.Armazenagem.Core.DTOs;
public class LogDTO
{
    public Guid? IdRequisicao { get; set; }

    public string? NmIdentificador { get; set; }

    public string? IdCaixa { get; set; }

    public DateTime Data { get; set; }

    public string Mensagem { get; set; } = string.Empty;

    public string Metodo { get; set; } = string.Empty;

    public string? IdOperador { get; set; }

    public string Tipo { get; set; } = string.Empty;
}
