namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class LogCaracolDTO
    {
        public long Id { get; set; }
        public string? IdRequisicao { get; set; } = string.Empty;
        public string? NmIdentificador { get; set; }
        public string? IdCaixa { get; set; }
        public DateTime? Data { get; set; }
        public string? Mensagem { get; set; }
        public string? Metodo { get; set; }
        public string? IdOperador { get; set; }
        public string? Tipo { get; set; }
    }
}
