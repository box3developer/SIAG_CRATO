namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class AgrupadorAtivoDTO
    {
        public Guid IdAgrupador { get; set; }

        public int? TpAgrupamento { get; set; }

        public string? Codigo1 { get; set; } = string.Empty;

        public string? Codigo2 { get; set; } = string.Empty;

        public string? Codigo3 { get; set; } = string.Empty;

        public int? CdSequencia { get; set; }

        public DateTime? DtAgrupador { get; set; }

        public int? IdAreaArmazenagem { get; set; }

        public int? FgStatus { get; set; }
    }
}
