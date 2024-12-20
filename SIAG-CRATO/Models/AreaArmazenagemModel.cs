using System.ComponentModel.DataAnnotations.Schema;
using SIAG_CRATO.Data;

namespace SIAG_CRATO.Models
{
    public class AreaArmazenagemModel
    {
        [Column("id_areaarmazenagem")]
        public long Codigo { get; set; }

        [Column("id_tipoarea")]
        public int TipoAreaId { get; set; }

        [Column("id_endereco")]
        public int EnderecoId { get; set; }

        [Column("id_agrupador")]
        public Guid AgrupadorId { get; set; }

        [Column("id_caracol")]
        public string? IdentificadorCaracol { get; set; }

        [Column("nr_posicaox")]
        public int PosicaoX { get; set; }

        [Column("nr_posicaoy")]
        public int PosicaoY { get; set; }

        [Column("nr_lado")]
        public int Lado { get; set; }

        [Column("fg_status")]
        public StatusAreaArmazenagem Status { get; set; }

        [Column("cd_identificacao")]
        public string Identificacao { get; set; } = string.Empty;
    }
}
