namespace SIAG_CRATO.Services
{
    public enum Rotina
    {
        AlocaPallet = 3,
        ValidaEnderecoExatoOrigem = 13,
        ValidaEAtribuiPalletNoEnderecoDestino = 16,
        VerificaPalletOrigemERemoveAlocacao = 17,
        AtribuiPalletNoEnderecoOrigem = 19,
        DefineUmaAreaDeDestinoParaArmazenagem = 20,
        LePalletDoBuffer = 21,
        LeStageIn = 22,
        ArmazenaPallet = 23,
        DefinePosicaoStageOut = 24,
        DefinePosicaoNaExpedicao = 30,
        DesalocaEndereco = 31,
        LeDocaDeExpedicao = 32,
        SempreOkNenhumaValidacao = 33,
        LeDocaDeExpedicaoParaOrdemInterna = 34,
        LeBufferParaOrdemDeTransferencia = 35
    }
}
