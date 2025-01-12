namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class ParametroMensagemCaracolDTO
    {
        public int IdParametromensagemcaracol { get; set; }

        public string? Cor { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public string Mensagem { get; set; } = string.Empty;
    }
}
