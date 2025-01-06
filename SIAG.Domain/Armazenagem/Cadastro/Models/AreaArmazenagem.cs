using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("areaarmazenagem")]
    public class AreaArmazenagem
    {
        [Key]
        [Column("id_areaarmazenagem")]
        public int IdAreaArmazenagem { get; set; }

        [ForeignKey("tipoarea")]
        [Column("id_tipoarea")]
        public int IdTipoArea { get; set; }
        public TipoArea? TipoArea { get; set; }

        [ForeignKey("endereco")]
        [Column("id_endereco")]
        public int IdEndereco { get; set; }
        public Endereco? Endereco { get; set; }

        [ForeignKey("agrupador")]
        [InverseProperty("areaarmazenagemprincipal")]
        [Column("id_agrupador")]
        public Guid? IdAgrupador { get; set; }
        public AgrupadorAtivo? Agrupador { get; set; }

        [Column("nr_posicaox")]
        public int NrPosicaoX { get; set; }

        [Column("nr_posicaoy")]
        public int NrPosicaoY { get; set; }

        [Column("nr_lado")]
        public int NrLado { get; set; }

        [Column("fg_status")]
        public int FgStatus { get; set; }

        [Column("cd_identificacao")]
        public string? CdIdentificacao { get; set; } = string.Empty;

        [ForeignKey("agrupadorreservado")]
        [InverseProperty("areaarmazenagemreservada")]
        [Column("id_agrupadorreservado")]
        public Guid? IdAgrupadorReservado { get; set; }
        public AgrupadorAtivo? AgrupadorReservado { get; set; }
    }
}
