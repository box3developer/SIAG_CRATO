using System.ComponentModel.DataAnnotations.Schema;
using SIAG_CRATO.Data;

namespace SIAG_CRATO.Models
{
    public class AreaArmazenagemModel
    {
        [Column("id_areaarmazenagem")]
        public long Id_areaarmazenagem { get; set; }

        [Column("id_tipoarea")]
        public int Id_tipoarea { get; set; }

        [Column("id_endereco")]
        public int Id_endereco { get; set; }

        [Column("id_agrupador")]
        public Guid Id_agrupador { get; set; }

        [Column("id_caracol")]
        public string? Id_caracol { get; set; }

        [Column("nr_posicaox")]
        public int Nr_posicaox { get; set; }

        [Column("nr_posicaoy")]
        public int Nr_posicaoy { get; set; }

        [Column("nr_lado")]
        public int Nr_lado { get; set; }

        [Column("fg_status")]
        public StatusAreaArmazenagem Fg_status { get; set; }

        [Column("cd_identificacao")]
        public string Cd_identificacao { get; set; } = string.Empty;
    }
}
