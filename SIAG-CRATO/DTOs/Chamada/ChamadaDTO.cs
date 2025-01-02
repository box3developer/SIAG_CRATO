using SIAG_CRATO.Data;

namespace SIAG_CRATO.DTOs.Chamada;

public class ChamadaDTO
{
    public Guid Codigo { get; set; }
    public int PalletOrigemId { get; set; }
    public int PalletDestinoId { get; set; }
    public int PalletLeituraId { get; set; }
    public long AreaArmazenagemOrigemId { get; set; }
    public long AreaArmazenagemDestinoId { get; set; }
    public long AreaArmazenagemLeituraId { get; set; }
    public long OperadorId { get; set; }
    public int EquipamentoId { get; set; }
    public int AtividadeRejeicaoId { get; set; }
    public int AtividadeId { get; set; }
    public StatusChamada Status { get; set; }
    public DateTime? DataChamada { get; set; }
    public DateTime? DataRecebida { get; set; }
    public DateTime? DataAtendida { get; set; }
    public DateTime? DataFinalizada { get; set; }
    public DateTime? DataRejeitada { get; set; }
    public DateTime? DataSuspensa { get; set; }
    public Guid CodigoChamadaSuspensa { get; set; }
}
