namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class TransportadoraTipoCargaDTO
    {
        public int IdTransportadoraTipoCarga { get; set; }

        public int IdTransportadora { get; set; }

        public string TpCarga { get; set; } = string.Empty;

        public string NmCarga { get; set; } = string.Empty;

        public string NmOrdem { get; set; } = string.Empty;
        
        public string Observacao { get; set; } = string.Empty;
    }
}