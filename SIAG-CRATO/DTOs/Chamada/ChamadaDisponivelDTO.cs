using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG_CRATO.DTOs.Chamada;

public class ChamadaDisponivelDTO
{
    [Column("id_chamada")]
    public Guid IdChamada { get; set; }

    [Column("dt_chamada")]
    public DateTime DataChamada { get; set; }

    [Column("id_atividade")]
    public int IdAtividade { get; set; }

    [Column("id_endereco_destino")]
    public int IdEnderecoDestino { get; set; }

    [Column("id_endereco_origem")]
    public int IdEnderecoOrigem { get; set; }

    [Column("qt_prioridade")]
    public int QuatidadePrioridade { get; set; }

    [Column("id_areaarmazenagemorigem")]
    public long IdAreaAmazenagemOrigem { get; set; }

    [Column("fg_processado")]
    public bool Processando { get; set; }

    [Column("priorizar")]
    public bool Priorizar { get; set; }
}
