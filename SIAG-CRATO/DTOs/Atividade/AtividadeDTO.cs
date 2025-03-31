using SIAG_CRATO.Data;

namespace SIAG_CRATO.DTOs.Atividade;

public class AtividadeDTO
{
    public int IdAtividade { get; set; }
    public string NmAtividade { get; set; } = string.Empty;
    public int IdEquipamentoModelo { get; set; }
    public int IdAtividadeRotinaValidacao { get; set; }
    public int IdAtividadeAnterior { get; set; }
    public int IdSetorTrabalho { get; set; }
    public RejeicaoTarefa FgPermiteRejeitar { get; set; }
    public TipoAtribuicaoAutomatica FgTipoAtribuicaoAutomatica { get; set; }
    public ConflitoDeEnderecos FgEvitaConflitoEndereco { get; set; }
    public TimeSpan Duracao { get; set; }
}
