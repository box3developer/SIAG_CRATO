using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models;

[KeylessEntity]
[Table("statusluzverde")]
public class StatusLuzVerde
{
    [Column("caracol")]
    public string Caracol { get; set; } = string.Empty;

    [Column("luz_verde")]
    public int LuzVerde { get; set; }
}
