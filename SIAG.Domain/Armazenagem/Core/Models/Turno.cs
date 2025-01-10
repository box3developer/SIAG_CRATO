using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Core.Models
{
    [BasicEntity]
    [Table("turno")]
    public class Turno
    {
        [Key]
        [Column("id_turno")]
        public int IdTurno { get; set; }

        [Column("cd_turno")]
        public int CdTurno { get; set; }

        [Column("dt_inicio")]
        public DateTime? DtInicio { get; set; }

        [Column("dt_fim")]
        public DateTime? DtFim { get; set; }

        [Column("diaanterior")]
        public bool? DiaAnterior { get; set; }

        [Column("diasucessor")]
        public bool? DiaSucessor { get; set; }
    }
}
