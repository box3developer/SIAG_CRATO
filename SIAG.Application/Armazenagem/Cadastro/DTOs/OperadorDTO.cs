namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class OperadorDTO
    {
        public long IdOperador { get; set; }

        public string? NmOperador { get; set; } = string.Empty;

        public string? NmCpf { get; set; } = string.Empty;

        public int? NrLocalidade { get; set; }

        public DateTime? DtLogin { get; set; }

        public int? FgFuncao { get; set; }

        public long? IdResponsavel { get; set; }

        public string? NmLogin { get; set; } = string.Empty;

        public string? NmNfcoperaddor { get; set; } = string.Empty;
    }
}
