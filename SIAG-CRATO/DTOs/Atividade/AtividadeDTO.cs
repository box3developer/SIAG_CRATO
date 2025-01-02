using SIAG_CRATO.Data;

namespace SIAG_CRATO.DTOs.Atividade;

public class AtividadeDTO
{
    public int Codigo { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public int EquipamentoModeloId { get; set; }
    public int AtividadeRotinaValidacaoId { get; set; }
    public int AtividadeAnteriorId { get; set; }
    public int SetorTrabalhoId { get; set; }
    public RejeicaoTarefa PermiteRejeitar { get; set; }
    public TipoAtribuicaoAutomatica TipoAtribuicaoAutomatica { get; set; }
    public ConflitoDeEnderecos EvitarConflitoEndereco { get; set; }
}
