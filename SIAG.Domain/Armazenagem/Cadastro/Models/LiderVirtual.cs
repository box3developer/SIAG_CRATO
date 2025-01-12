using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [CustomKeyEntity]
    [Table("lidervirtual")]
    public class LiderVirtual
    {
        [Key]
        [Column("id_lidervirtual")]
        public long IdLidervirtual { get; set; }

        [Column("dt_login")]
        public DateTime? DtLogin { get; set; }

        [Column("dt_loginlimite")]
        public DateTime? DtLoginlimite { get; set; }

        [Column("dt_logoff")]
        public DateTime? DtLogoff { get; set; }

        [Column("id_equipamentodestino")]
        public int? IdEquipamentoDestino { get; set; }

        [ForeignKey(nameof(IdEquipamentoDestino))]
        public Equipamento? EquipamentoDestino { get; set; }


        [Column("id_equipamentoorigem")]
        public int? IdEquipamentoOrigem { get; set; }

        [ForeignKey(nameof(IdEquipamentoOrigem))]
        public Equipamento? EquipamentoOrigem { get; set; }


        [Column("id_operador")]
        public long? IdOperador { get; set; }

        [ForeignKey(nameof(IdOperador))]
        public Operador? Operador { get; set; }


        [Column("id_operadorlogin")]
        public long? IdOperadorlogin { get; set; }

        [ForeignKey(nameof(IdOperadorlogin))]
        public Operador? OperadorLogin { get; set; }

    }

}
