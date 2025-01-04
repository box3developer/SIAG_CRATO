using System.ComponentModel.DataAnnotations;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    public class Turno
    {
        [Key]
        public int TurnoId { get; set; }

        public int CdTurno { get; set; }

        public DateTime DtInicio { get; set; }

        public DateTime DtFim { get; set; }

        public bool DiaAnterior { get; set; }

        public bool DiaSucessor { get; set; }
    }
}
