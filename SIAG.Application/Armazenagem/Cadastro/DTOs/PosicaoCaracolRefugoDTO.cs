namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class PosicaoCaracolRefugoDTO
    {
        public int IdPosicaocaracolrefugo { get; set; }

        public string? Descricao { get; set; } = string.Empty;

        public string? Fabrica { get; set; } = string.Empty;

        public int? Posicao { get; set; }

        public string? Tipo { get; set; } = string.Empty;
    }
}
