using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("turno")]
    public class Turno
    {
        [Key]
        [Column("id_turno")]
        public int TurnoId { get; set; }

        [Column("cd_turno")]
        public int CdTurno { get; set; }

        [Column("dt_inicio")]
        public DateTime DtInicio { get; set; }

        [Column("dt_fim")]
        public DateTime DtFim { get; set; }

        [Column("diaanterior")]
        public bool DiaAnterior { get; set; }

        [Column("diasucessor")]
        public bool DiaSucessor { get; set; }
    }
}
