using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.DTOs.Chamada;

public class ChamadaDisponivelDTO
{
    [Column("id_chamada")]
    public Guid ChamadaId { get; set; }

    [Column("dt_chamada")]
    public DateTime DataChamada { get; set; }

    [Column("id_atividade")]
    public int AtividadeId { get; set; }

    [Column("id_endereco_destino")]
    public int EnderecoDestinoId { get; set; }

    [Column("id_endereco_origem")]
    public int EnderecoOrigemId { get; set; }

    [Column("qt_prioridade")]
    public int QuatidadePrioridade { get; set; }

    [Column("id_areaarmazenagemorigem")]
    public long AreaAmazenagemOrigemId { get; set; }

    [Column("fg_processado")]
    public bool Processando { get; set; }

    [Column("priorizar")]
    public bool Priorizar { get; set; }
}
