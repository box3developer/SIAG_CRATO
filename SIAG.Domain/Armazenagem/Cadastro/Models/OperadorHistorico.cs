using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("operadorhistorico")]
    public class Operadorhistorico
    {
        [Key]
        [Column("id_operadorhistorico")]
        public int OperadorHistorico { get; set; }

        [Column("id_operador")]
        public long IdOperador { get; set; }

        [Column("cd_evento")]
        public int CdEvento { get; set; }

        [Column("dt_evento")]
        public DateTime DtEvento { get; set; }

        [Column("id_endereco")]
        public int IdEndereco { get; set; }

        [Column("id_equipamento")]
        public int IdEquipamento { get; set; }
    }
}
