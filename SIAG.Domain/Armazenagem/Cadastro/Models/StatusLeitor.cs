using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models

{
    [BasicEntity]
    [Table("status_leitor")]
    public class Status_leitor
    {
        [Key]
        [Column("id_status_leitor")]
        public int IdStatusLeitor { get; set; }

        [Column("conectado")]
        public int? Conectado { get; set; }

        [Column("configurado")]
        public int? Configurado { get; set; }

        [Column("dt_status")]
        public DateTime? DtStatus { get; set; }

        [Column("equipamento")]
        public string Equipamento { get; set; } = string.Empty;

        [Column("executando")]
        public int? Executando { get; set; }

        [Column("leitor")]
        public string? Leitor { get; set; } = string.Empty;

    }

}
