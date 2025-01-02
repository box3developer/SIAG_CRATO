using SIAG_CRATO.Data;

namespace SIAG_CRATO.DTOs.AreaArmazenagem;

public class AreaArmazenagemDTO
{
    public long AreaArmazenagemId { get; set; }
    public int TipoAreaId { get; set; }
    public int EnderecoId { get; set; }
    public Guid AgrupadorId { get; set; }
    public string? CaracolId { get; set; }
    public int PosicaoX { get; set; }
    public int PosicaoY { get; set; }
    public int Lado { get; set; }
    public StatusAreaArmazenagem Status { get; set; }
    public string Identificacao { get; set; } = string.Empty;
}
