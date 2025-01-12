namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class ParametroDTO
    {
        public int IdParametro { get; set; }

        public int? FgAtivo { get; set; }

        public int? FgTipoparametro { get; set; }

        public bool? FgVisivel { get; set; }

        public string? NmParametro { get; set; } = string.Empty;

        public string? NmTipo { get; set; } = string.Empty;

        public string? NmUnidademedida { get; set; } = string.Empty;

        public string? NmValor { get; set; } = string.Empty;
    }
}
