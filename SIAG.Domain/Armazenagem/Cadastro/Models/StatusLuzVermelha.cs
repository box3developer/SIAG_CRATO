using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models;

[KeylessEntity]
[Table("statusluzvermelha")]
public class StatusLuzVermelha
{
    [Column("caracol")]
    public string Caracol { get; set; } = string.Empty;

    [Column("luz_vermelha")]
    public List<int> LuzesVM { get; set; }
}
