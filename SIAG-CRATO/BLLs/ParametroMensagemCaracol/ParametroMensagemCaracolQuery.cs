namespace SIAG_CRATO.BLLs.ParametroMensagemCaracol;

public class ParametroMensagemCaracolQuery
{
    public const string SELECT = @"SELECT id_parametromensagemcaracol, descricao, mensagem, cor FROM parametromensagemcaracol WITH(NOLOCK)";
}
