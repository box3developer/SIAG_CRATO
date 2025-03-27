namespace SIAG.Application.Armazenagem.Core.DTOs
{
    public class PalletDTO
    {
        public int IdPallet { get; set; }

        public long? IdAreaArmazenagem { get; set; }

        public AreaArmazenagemDTO? AreaArmazenagem { get; set; }

        public Guid? IdAgrupadorAtivo { get; set; }

        public AgrupadorAtivoDTO? AgrupadorAtivo { get; set; }

        public int? FgStatus { get; set; }

        public int? QtUtilizacao { get; set; }

        public DateTime? DtUltimaMovimentacao { get; set; }

        public string? CdIdentificacao { get; set; } = string.Empty;
    }
}
