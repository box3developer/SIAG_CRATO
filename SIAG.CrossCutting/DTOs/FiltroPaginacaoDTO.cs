namespace SIAG.CrossCutting.DTOs
{
    public class FiltroPaginacaoDTO
    {
        public string Pesquisa { get; set; } = string.Empty;
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public bool? Ativo { get; set; }
        public bool Impressao { get; set; }
    }
}
