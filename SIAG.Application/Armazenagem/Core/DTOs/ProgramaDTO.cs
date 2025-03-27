namespace SIAG.Application.Armazenagem.Core.DTOs
{
    public class ProgramaDTO
    {
        public int IdPrograma { get; set; }

        public int? CdPrograma { get; set; }

        public int? CdDocumento { get; set; }

        public int CdFabrica { get; set; }

        public int CdEstabelecimento { get; set; }

        public string CdEquipamento { get; set; } = string.Empty;

        public DateTime DtLiberacao { get; set; }

        public int FgTipo { get; set; }

        public string? CdDeposito { get; set; } = string.Empty;

        public decimal? QtAlturaCaixa { get; set; }

        public decimal? QtLarguraCaixa { get; set; }

        public decimal? QtComprimentoCaixa { get; set; }
    }
}
