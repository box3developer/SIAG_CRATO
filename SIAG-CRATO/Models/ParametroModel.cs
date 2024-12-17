using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.Models
{
    public class ParametroModel
    {
        [Column("id_parametro")]
        public int Id { get; set;}

        [Column("nm_parametro")]
        public string? Parametro { get; set;}

        [Column("nm_valor")]
        public string? Valor { get; set; }

        [Column("fg_tipoparametro")]
        public string? TipoParametro { get; set; }

        [Column("nm_unidademedida")]
        public string? Unidade { get; set; }

        [Column("nm_tipo")]
        public string? Tipo { get; set; }

        [Column("fg_visivel")]
        public bool? Visivel { get; set; }

        [Column("fg_ativo")]
        public int? Ativo { get; set; }
    }
}
