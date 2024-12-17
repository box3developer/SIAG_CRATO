using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.Models;

public class CaixaLeituraModel
{
    [Column("id_caixaleitura")]
    public string? IdCaixaLeitura { get; set; }

    [Column("id_caixa")]
    public string? IdCaixa { get; set; }

    [Column("dt_leitura")]
    public DateTime DtLeitura { get; set; }

    [Column("fg_tipo")]
    public int? FgTipo { get; set; }

    [Column("fg_status")]
    public int? FgStatus { get; set; }

    [Column("id_operador")]
    public string? IdOperador { get; set; }

    [Column("id_equipamento")]
    public string? IdEquipamento { get; set; }

    [Column("id_pallet")]
    public string? IdPallet { get; set; }

    [Column("id_areaarmazenagem")]
    public string? IdAreaArmazenagem { get; set; }

    [Column("id_endereco")]
    public string? IdEndereco { get; set; }

    [Column("fg_cancelado")]
    public int? FgCancelado { get; set; }

    [Column("id_ordem")]
    public string? IdOrdem { get; set; }
}