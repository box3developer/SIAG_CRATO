using System.ComponentModel.DataAnnotations.Schema;
using SIAG_CRATO.Data;

namespace SIAG_CRATO.Models;
public class EquipamentoModel
{
    [Column("id_equipamento")]
    public int Codigo { get; set; }

    [Column("id_equipamentomodelo")]
    public int ModeloId { get; set; }

    [Column("id_setortrabalho")]
    public int? SetorId { get; set; }

    [Column("id_operador")]
    public int OperadorId { get; set; }

    [Column("nm_equipamento")]
    public string Descricao { get; set; } = string.Empty;

    [Column("nm_abreviado_equipamento")]
    public string DescricaoAbreviada { get; set; } = string.Empty;

    [Column("nm_identificador")]
    public string Identificador { get; set; } = string.Empty;

    [Column("fg_status")]
    public StatusEquipamento Status { get; set; }

    [Column("dt_inclusao")]
    public DateTime? DataInclusao { get; set; }

    [Column("dt_manutencao")]
    public DateTime? DataManutencao { get; set; }

    [Column("dt_ultimaleitura")]
    public DateTime? DataUltimaLeitura { get; set; }

    [Column("id_endereco")]
    public EnderecoModel? EnderecoTrabalho { get; set; }

    [Column("nm_ip")]
    public string IP { get; set; } = string.Empty;

    [Column("fg_statustrocacaracol")]
    public Ativo StatusTrocaCaracol { get; set; }

    public string UltimaLeitura { get; set; } = string.Empty;

    public string Observacao { get; set; } = string.Empty;
}
