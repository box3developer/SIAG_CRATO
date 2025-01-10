using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Core.Models
{
    [CustomKeyEntity]
    [Table("operador")]
    public class Operador
    {
        [Key]
        [Column("id_operador")]
        public long IdOperador { get; set; }

        [Column("nm_operador")]
        public string? NmOperador { get; set; } = string.Empty;

        [Column("nm_cpf")]
        public string? NmCpf { get; set; } = string.Empty;

        [Column("nr_localidade")]
        public int? NrLocalidade { get; set; }

        [Column("dt_login")]
        public DateTime? DtLogin { get; set; }

        [Column("fg_funcao")]
        public int? FgFuncao { get; set; }

        [Column("id_responsavel")]
        public long? IdResponsavel { get; set; }

        [Column("nm_login")]
        public string? NmLogin { get; set; } = string.Empty;

        [Column("nm_nfcoperador")]
        public string? NmNfcoperaddor { get; set; } = string.Empty;
    }
}
