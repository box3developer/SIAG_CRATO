using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("agrupadorativo")]
    public class AgrupadorAtivo
    {
        [Key]
        [Column("id_agrupador")]
        public Guid AgrupadorId { get; set; }

        [Column("tp_agrupamento")]
        public int TpAgrupamento { get; set; }

        [Column("codigo1")]
        public string Codigo1 { get; set; }

        [Column("codigo2")]
        public string Codigo2 { get; set; }

        [Column("codigo3")]
        public string Codigo3 { get; set; }

        [Column("cd_sequencia")]
        public int CdSequencia { get; set; }

        [Column("dt_agrupador")]
        public DateTime DtAgrupador { get; set; }

        [Column("id_areaarmazenagem")]
        public int AreaArmazenagemId { get; set; }

        [Column("fg_status")]
        public int FgStatus { get; set; }
    }
}
