using SIAG.Domain.Armazenagem.Attributes;
using SIAG.Domain.Armazenagem.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [BasicEntity]
    [Table("transportadoratipocarga")]
    public class TransportadoraTipoCarga
    {
        [Key]
        [Column("id_transportadoratipocarga")]
        public int IdTransportadoraTipoCarga { get; set; }

        [Column("id_transportadora")]
        public int IdTransportadora { get; set; }

        [Column("tp_carga")]
        public string TpCarga { get; set; } = string.Empty;

        [Column("nm_carga")]
        public string NmCarga { get; set; } = string.Empty;

        [Column("nm_ordem")]
        public string NmOrdem { get; set; } = string.Empty;
        
        [Column("observacao")]
        public string Observacao { get; set; } = string.Empty;
    }
}
