using SIAG_CRATO.Data;

namespace SIAG_CRATO.Models;
public class EquipamentoModel
{
    public int IdEquipamento { get; set; }
    public int IdEquipamentoModelo { get; set; }
    public int? IdSetorTrabalho { get; set; }
    public int IdOperador { get; set; }
    public string NmEquipamento { get; set; } = string.Empty;
    public string NmAbreviadoEquipamento { get; set; } = string.Empty;
    public string NmIdentificador { get; set; } = string.Empty;
    public StatusEquipamento FgStatus { get; set; }
    public DateTime? DtInclusao { get; set; }
    public DateTime? DtManutencao { get; set; }
    public DateTime? DtUltimaLeitura { get; set; }
    public EnderecoModel? IdEndereco { get; set; }
    public string NmIP { get; set; } = string.Empty;
    public Ativo FgStatusTrocaCaracol { get; set; }
    public string CdLeituraPendente { get; set; } = string.Empty;
    public string CdUltimaLeitura { get; set; } = string.Empty;
    public string Observacao { get; set; } = string.Empty;
}
