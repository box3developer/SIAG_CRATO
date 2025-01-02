using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.Models;

public class CaixaLeituraModel
{
    public int? Id_caixaleitura { get; set; }

    public string? Id_caixa { get; set; }

    public DateTime Dt_leitura { get; set; }

    public int? Fg_tipo { get; set; }

    public int? Fg_status { get; set; }

    public int? Id_operador { get; set; }

    public int? Id_equipamento { get; set; }

    public int? Id_pallet { get; set; }

    public long? Id_areaarmazenagem { get; set; }

    public int? Id_endereco { get; set; }

    public int? Fg_cancelado { get; set; }

    public int? Id_ordem { get; set; }
}