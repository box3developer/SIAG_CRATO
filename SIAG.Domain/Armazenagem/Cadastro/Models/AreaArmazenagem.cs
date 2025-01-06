using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("areaarmazenagem")]
    public class AreaArmazenagem
    {
        [Key]
        [Column("idarea_armazenagem")]
        public int IdAreaArmazenagem { get; set; }

        [ForeignKey("TipoArea")]
        [Column("id_tipo_area")]
        public int IdTipoArea { get; set; }
        public TipoArea? TipoArea { get; set; }

        [ForeignKey("Endereco")]
        [Column("id_endereco")]
        public int IdEndereco { get; set; }
        public Endereco? Endereco { get; set; }

        [ForeignKey("Agrupador")]
        [InverseProperty("AreaArmazenagemPrincipal")]
        [Column("agrupador_id")]
        public Guid AgrupadorId { get; set; }
        public AgrupadorAtivo? Agrupador { get; set; }

        [Column("nr_posicao_x")]
        public int NrPosicaoX { get; set; }

        [Column("nr_posicao_y")]
        public int NrPosicaoY { get; set; }

        [Column("nr_lado")]
        public int NrLado { get; set; }

        [Column("fg_status")]
        public int FgStatus { get; set; }

        [Column("cd_identificacao")]
        public string CdIdentificacao { get; set; } = string.Empty;

        [ForeignKey("AgrupadorReservado")]
        [InverseProperty("AreaArmazenagemReservada")]
        [Column("id_agrupador_reservado")]
        public Guid IdAgrupadorReservado { get; set; }
        public AgrupadorAtivo? AgrupadorReservado { get; set; }
    }
}
