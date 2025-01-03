using SIAG_CRATO.Data;

namespace SIAG_CRATO.DTOs.Chamada;

public class ChamadaDTO
{
    public Guid IdChamada { get; set; }
    public int IdPalletOrigem { get; set; }
    public int IdPalletDestino { get; set; }
    public int IdPalletLeitura { get; set; }
    public long IdAreaArmazenagemOrigem { get; set; }
    public long IdAreaArmazenagemDestino { get; set; }
    public long IdAreaArmazenagemLeitura { get; set; }
    public long IdOperador { get; set; }
    public int IdEquipamento { get; set; }
    public int IdAtividadeRejeicao { get; set; }
    public int IdAtividade { get; set; }
    public StatusChamada FgStatus { get; set; }
    public DateTime? DtChamada { get; set; }
    public DateTime? DtRecebida { get; set; }
    public DateTime? DtAtendida { get; set; }
    public DateTime? DtFinalizada { get; set; }
    public DateTime? DtRejeitada { get; set; }
    public DateTime? DtSuspensa { get; set; }
    public Guid IdChamadaSuspensa { get; set; }
}
