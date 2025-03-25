using SIAG.Domain.Armazenagem.Attributes;
using SIAG.Domain.Armazenagem.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [KeylessEntity]
    [Table("chamadatarefa")]
    public class ChamadaTarefa
    {
        [Column("id_chamada")]
        public Guid IdChamada { get; set; }

        [ForeignKey(nameof(IdChamada))]
        public Chamada? Chamada { get; set; }


        [Column("id_tarefa")]
        public int IdTarefa{ get; set; }

        [ForeignKey(nameof(IdTarefa))]
        public AtividadeTarefa? Tarefa { get; set; }


        [Column("dt_inicio")]
        public DateTime DtInicio { get; set; }

        [Column("dt_fim")]
        public DateTime DtFim{ get; set; }
    }
}
