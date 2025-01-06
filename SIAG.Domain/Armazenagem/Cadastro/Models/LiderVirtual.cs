using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("lidervirtual")]
    public class Lidervirtual
    {
        [Key]
        [Column("id_lidervirtual")]
        public long IdLidervirtual { get; set; }

        [Column("dt_login")]
        public DateTime DtLogin { get; set; }

        [Column("dt_loginlimite")]
        public DateTime DtLoginlimite { get; set; }

        [Column("dt_logoff")]
        public DateTime DtLogoff { get; set; }

        [Column("id_equipamentodestino")]
        public int IdEquipamentodestino { get; set; }

        [Column("id_equipamentoorigem")]
        public int IdEquipamentoorigem { get; set; }

        [Column("id_operador")]
        public long IdOperador { get; set; }

        [Column("id_operadorlogin")]
        public long IdOperadorlogin { get; set; }

    }

}
