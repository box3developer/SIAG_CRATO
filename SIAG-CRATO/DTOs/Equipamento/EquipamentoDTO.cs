using SIAG_CRATO.Data;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.DTOs.Equipamento;

public class EquipamentoDTO
{
    public int Codigo { get; set; }
    public int ModeloId { get; set; }
    public int? SetorId { get; set; }
    public int OperadorId { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public string DescricaoAbreviada { get; set; } = string.Empty;
    public string Identificador { get; set; } = string.Empty;
    public StatusEquipamento Status { get; set; }
    public DateTime? DataInclusao { get; set; }
    public DateTime? DataManutencao { get; set; }
    public DateTime? DataUltimaLeitura { get; set; }
    public EnderecoModel? EnderecoTrabalho { get; set; }
    public string IP { get; set; } = string.Empty;
    public Ativo StatusTrocaCaracol { get; set; }
    public string LeituraPendete { get; set; } = string.Empty;
    public string UltimaLeitura { get; set; } = string.Empty;
    public string Observacao { get; set; } = string.Empty;

}
