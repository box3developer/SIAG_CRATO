﻿namespace SIAG_CRATO.DTOs.LiderVirtual;

public class LiderVirtualDTO
{
    public string? IdLiderVirtual { get; set; }
    public string? IdOperador { get; set; }
    public string? IdEquipamentoOrigem { get; set; }
    public string? IdEquipamentoDestino { get; set; }
    public DateTime? DtLogin { get; set; }
    public DateTime? DtLogoff { get; set; }
    public string? IdOperadorLogin { get; set; }
    public DateTime? DtLoginLimite { get; set; }
}
