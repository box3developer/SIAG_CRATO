﻿using SIAG_CRATO.Data;

namespace SIAG_CRATO.DTOs.AtividadeRotina;

public class AtividadeRotinaDTO
{
    public int IdAtividadeRotina { get; set; }
    public string NmAtividadeRotina { get; set; } = string.Empty;
    public string NmProcedure { get; set; } = string.Empty;
    public TipoRotina FgTipo { get; set; }
}