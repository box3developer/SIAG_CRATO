using SIAG.Domain.Armazenagem.Cadastro.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [CustomKeyEntity]
    [Table("areaarmazenagem")]
    public class AreaArmazenagem
    {
        [Key]
        [Column("id_areaarmazenagem")]
        public long IdAreaArmazenagem { get; set; }


        [Column("id_tipoarea")]
        public int IdTipoArea { get; set; }

        [ForeignKey(nameof(IdTipoArea))]
        public TipoArea? TipoArea { get; set; }


        [Column("id_endereco")]
        public int IdEndereco { get; set; }

        [ForeignKey(nameof(IdEndereco))]
        public Endereco? Endereco { get; set; }


        [Column("id_agrupador")]
        public Guid? IdAgrupador { get; set; }

        [ForeignKey(nameof(IdAgrupador))]
        public AgrupadorAtivo? Agrupador { get; set; }


        [Column("nr_posicaox")]
        public int NrPosicaoX { get; set; }

        [Column("nr_posicaoy")]
        public int NrPosicaoY { get; set; }

        [Column("nr_lado")]
        public int? NrLado { get; set; }

        [Column("fg_status")]
        public int FgStatus { get; set; }

        [Column("cd_identificacao")]
        public string? CdIdentificacao { get; set; } = string.Empty;


        [Column("id_agrupador_reservado")]
        public Guid? IdAgrupadorReservado { get; set; }

        [ForeignKey(nameof(IdAgrupadorReservado))]
        public AgrupadorAtivo? AgrupadorReservado { get; set; }
    }
}
