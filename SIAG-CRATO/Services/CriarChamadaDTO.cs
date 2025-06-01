namespace SIAG_CRATO.Services
{
    public class CriarChamadaDTO
    {
        public int? IdAtividade { get; set; }

        public int? IdPalletOrigem { get; set; }

        public long? IdAreaArmazenagemOrigem { get; set; }

        public int? IdPalletDestino { get; set; }

        public long? IdAreaArmazenagemDestino { get; set; }

        public bool? Priorizar { get; set; } = false;
    }
}
