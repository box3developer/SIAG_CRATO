using SIAG.Domain.Armazenagem.Attributes;
using SIAG.Domain.Armazenagem.Cadastro.Models;
using SIAG.Domain.Armazenagem.Core.Models;
using SIAG.Domain.Chamada.Cadastro.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Chamada.Core.Models
{
    [BasicEntity]
    [Table("historicopallet")]
    public class HistoricoPallet
    {
        [Key]
        [Column("id_historicopallet")]
        public int IdHistoricoPallet { get; set; }


        [Column("id_responsabilidade")]
        public int? IdResponsabilidade { get; set; }

        
        [Column("id_atividade")]
        public int? IdAtividade { get; set; }

        [ForeignKey(nameof(IdAtividade))]
        public Atividade? Atividade { get; set; }


        [Column("id_chamada")]
        public Guid? IdChamada { get; set; }

        [ForeignKey(nameof(IdChamada))]
        public Chamada? Chamada { get; set; }


        [Column("id_areaarmazenagem")]
        public long? IdAreaarmazenagem { get; set; }

        [ForeignKey(nameof(IdAreaarmazenagem))]
        public AreaArmazenagem? Areaarmazenagem { get; set; }

        [Column("dt_evento")]
        public DateTime? DtEvento { get; set; }

        [Column("id_pallet")]
        public int? IdPallet { get; set; }

        [ForeignKey(nameof(IdPallet))]
        public Pallet? Pallet { get; set; }

        [Column("id_operador")]
        public long? IdOperador { get; set; }

        [ForeignKey(nameof(IdOperador))]
        public Operador? Operador { get; set; }

        [Column("nm_historico")]
        public int NmHistorico { get; set; }
    }
}
