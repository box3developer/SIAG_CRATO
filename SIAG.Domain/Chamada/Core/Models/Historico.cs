using SIAG.Domain.Armazenagem.Attributes;
using SIAG.Domain.Armazenagem.Cadastro.Models;
using SIAG.Domain.Armazenagem.Core.Models;
using SIAG.Domain.Chamada.Cadastro.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Chamada.Core.Models
{
    [KeylessEntity]
    [Table("historico")]
    public class Historico
    {
        [Column("dt_historico")]
        public DateTime? DtEvento { get; set; }

        [Column("nm_usuario")]
        public string? NmUsuario { get; set; } = string.Empty;

        [Column("nm_objeto")]
        public string? NmObjeto { get; set; } = string.Empty;

        [Column("id_registro")]
        public int? IdRegistro { get; set; }

        [Column("ds_operacao")]
        public int? DsOperacao { get; set; }
    }
}
