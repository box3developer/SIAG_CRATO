namespace SIAG_CRATO.DTOs.StatusLeitor;

public class StatusLeitorDTO
{
    public string Equipamento { get; set; } = string.Empty;
    public string Leitor { get; set; } = string.Empty;
    public bool Configurado { get; set; }
    public bool Conectado { get; set; }
    public bool Executando { get; set; }
    public DateTime DtStatus { get; set; }
}
