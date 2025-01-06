using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("atividade")]
    public class Atividade
    {
        [Key]
        [Column("id_atividade")]
        public int IdAtividade { get; set; }

        [Column("duracao")]
        public TimeSpan? Duracao { get; set; }

        [Column("fg_evitaconflitoendereco")]
        public int FgEvitaconflitoendereco { get; set; }

        [Column("fg_permite_rejeitar")]
        public int FgPermiteRejeitar { get; set; }

        [Column("fg_tipoatribuicaoautomatica")]
        public int FgTipoatribuicaoautomatica { get; set; }

        [Column("id_atividadeanterior")]
        public int? IdAtividadeanterior { get; set; }

        [Column("id_atividaderotinavalidacao")]
        public int? IdAtividaderotinavalidacao { get; set; }

        [Column("id_equipamentomodelo")]
        public int IdEquipamentomodelo { get; set; }

        [Column("id_setortrabalho")]
        public int IdSetortrabalho { get; set; }

        [Column("nm_atividade")]
        public string NmAtividade { get; set; } = string.Empty;

    }
}
