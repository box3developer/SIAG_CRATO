namespace SIAG.Application.Armazenagem.Core.DTOs;

public class StatusLuzVermelhaDTO
{
    public string Caracol { get; set; } = string.Empty;

    public List<int> LuzesVM { get; set; }
}
