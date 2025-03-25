using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Core.Models
{
    [BasicEntity]
    [Table("logcaracol")]
    public class LogCaracol
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("id_requisicao")]
        public string? IdRequisicao { get; set; } = string.Empty;

        [Column("nm_identificador")]
        public string? NmIdentificador { get; set; }

        [Column("id_caixa")]
        public string? IdCaixa { get; set; }

        [Column("data")]
        public DateTime? Data { get; set; }

        [Column("mensagem")]
        public string? Mensagem { get; set; }

        [Column("metodo")]
        public string? Metodo { get; set; }

        [Column("id_operador")]
        public string? IdOperador { get; set; }

        [Column("tipo")]
        public string? Tipo { get; set; }
    }
}
