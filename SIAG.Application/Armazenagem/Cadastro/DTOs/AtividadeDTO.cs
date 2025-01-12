namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class AtividadeDTO
    {
        public int IdAtividade { get; set; }
        public string? NmAtividade { get; set; } = string.Empty;
        public int IdEquipamentomodelo { get; set; }
        public int? IdSetortrabalho { get; set; }
        public int? FgPermiteRejeitar { get; set; }
        public int? IdAtividadeanterior { get; set; }
        public int? FgTipoatribuicaoautomatica { get; set; }
        public int? IdAtividaderotinavalidacao { get; set; }
        public int? FgEvitaconflitoendereco { get; set; }
        public TimeSpan? Duracao { get; set; }
    }
}
