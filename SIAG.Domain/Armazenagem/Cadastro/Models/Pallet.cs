using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using SIAG.Domain.Armazenagem.Cadastro.Attributes;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [CustomKeyEntity]
    [Table("pallet")]
    public class Pallet
    {
        [Key]
        [Column("id_pallet")]
        public int IdPallet { get; set; }

        [Column("id_areaarmazenagem")]
        public long? IdAreaArmazenagem { get; set; }

        [ForeignKey(nameof(IdAreaArmazenagem))]
        public AreaArmazenagem? AreaArmazenagem { get; set; }

        [Column("id_agrupador")]
        public Guid? IdAgrupadorAtivo { get; set; }

        [ForeignKey(nameof(IdAgrupadorAtivo))]
        public AgrupadorAtivo? AgrupadorAtivo { get; set; }

        [Column("fg_status")]
        public int? FgStatus { get; set; }

        [Column("qt_utilizacao")]
        public int? QtUtilizacao { get; set; }

        [Column("dt_ultimamovimentacao")]
        public DateTime? DtUltimaMovimentacao { get; set; }

        [Column("cd_identificacao")]
        public string? CdIdentificacao { get; set; } = string.Empty;
    }
}
