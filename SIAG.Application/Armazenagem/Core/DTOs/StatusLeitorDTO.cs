namespace SIAG.Application.Armazenagem.Core.DTOs
{
    public class StatusLeitorDTO
    {
        public int IdStatusLeitor { get; set; }

        public int? Conectado { get; set; }

        public int? Configurado { get; set; }

        public DateTime? DtStatus { get; set; }

        public string Equipamento { get; set; } = string.Empty;

        public int? Executando { get; set; }

        public string? Leitor { get; set; } = string.Empty;
    }
}
