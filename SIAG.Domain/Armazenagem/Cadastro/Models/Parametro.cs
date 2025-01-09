using SIAG.Domain.Armazenagem.Cadastro.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [BasicEntity]
    [Table("parametro")]
    public class Parametro
    {
        [Key]
        [Column("id_parametro")]
        public int IdParametro { get; set; }

        [Column("fg_ativo")]
        public int? FgAtivo { get; set; }

        [Column("fg_tipoparametro")]
        public int? FgTipoparametro { get; set; }

        [Column("fg_visivel")]
        public bool? FgVisivel { get; set; }

        [Column("nm_parametro")]
        public string? NmParametro { get; set; } = string.Empty;

        [Column("nm_tipo")]
        public string? NmTipo { get; set; } = string.Empty;

        [Column("nm_unidademedida")]
        public string? NmUnidademedida { get; set; } = string.Empty;

        [Column("nm_valor")]
        public string? NmValor { get; set; } = string.Empty;

    }
}
