using System.ComponentModel.DataAnnotations.Schema;
using SIAG_CRATO.Data;

namespace SIAG_CRATO.Models;
public class OperadorModel
{
    [Column("id_operador")]
    public long Codigo { get; set; }

    public string NFC { get; set; } = string.Empty;

    [Column("nm_cpf")]
    public string CPF { get; set; } = string.Empty;

    [Column("nm_operador")]
    public string Descricao { get; set; } = string.Empty;

    [Column("dt_login")]
    public DateTime? DataLogin { get; set; }

    [Column("nr_localidade")]
    public Estabelecimentos Localidade { get; set; }

    [Column("fg_funcao")]
    public FuncaoOperador FuncaoOperador { get; set; }

    [Column("id_responsavel")]
    public int ResponsavelId { get; set; }
    public OperadorModel? Responsavel { get; set; }
}
