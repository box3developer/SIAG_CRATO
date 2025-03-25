using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Core.Models;


[KeylessEntity]
[Table("log")]
public class Log
{
    [Column("id_requisicao")]
    public Guid? IdRequisicao { get; set; }

    [Column("nm_identificador")]
    public string? NmIdentificador { get; set; }

    [Column("id_caixa")]
    public string? IdCaixa { get; set; }

    [Column("data")]
    public DateTime Data { get; set; }

    [Column("mensagem")]
    public string Mensagem { get; set; } = string.Empty;

    [Column("metodo")]
    public string Metodo { get; set; } = string.Empty;

    [Column("id_operador")]
    public string? IdOperador { get; set; }

    [Column("tipo")]
    public string Tipo { get; set; } = string.Empty;
}
