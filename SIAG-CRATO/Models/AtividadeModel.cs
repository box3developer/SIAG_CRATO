using SIAG_CRATO.Data;

namespace SIAG_CRATO.Models;
public class AtividadeModel
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
}
