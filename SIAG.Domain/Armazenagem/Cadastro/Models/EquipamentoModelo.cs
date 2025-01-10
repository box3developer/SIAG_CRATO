using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [BasicEntity]
    [Table("equipamentomodelo")]
    public class EquipamentoModelo
    {
        [Key]
        [Column("id_equipamentomodelo")]
        public int IdEquipamentoModelo { get; set; }

        [Column("nm_descricao")]
        public string NmDescricao { get; set; } = string.Empty;

        [Column("fg_status")]
        public int? FgStatus { get; set; }
    }
}
