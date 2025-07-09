using SIAG_CRATO.Repositories.Interfaces;
using SIAG_CRATO.Services;

public class RotinaService
{
    private readonly IAtividadeRotinaRepository _repository;

    public RotinaService(IAtividadeRotinaRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<ValidacaoEnderecoResult> ExecutarRotinaAsync(Rotina rotina, Guid idChamada)
    {
        switch (rotina)
        {
            case Rotina.AlocaPallet:
                return await _repository.AlocarPalletAsync(idChamada);

            case Rotina.ValidaEnderecoExatoOrigem:
                return await _repository.ValidarEnderecoExatoOrigemAsync(idChamada);

            case Rotina.ValidaEAtribuiPalletNoEnderecoDestino:
                return await _repository.AlocarPalletEnderecoAsync(idChamada);

            case Rotina.VerificaPalletOrigemERemoveAlocacao:
                return await _repository.VerificaDesalocaPalletOrigemAsync(idChamada);

            case Rotina.AtribuiPalletNoEnderecoOrigem:
                {
                    _ = await _repository.AlocarPalletLivreAsync(idChamada);
                    var palletStageIn = await _repository.LeituraBufferAsync(idChamada);

                    return palletStageIn;
                }

            case Rotina.LePalletDoBuffer:
                return await _repository.LeituraBufferAsync(idChamada);

            case Rotina.LeStageIn:
                return await _repository.LeituraStageInAsync(idChamada);

            case Rotina.ArmazenaPallet:
                return await _repository.ArmazenarPalletAsync(idChamada);

            case Rotina.DefinePosicaoStageOut:
                return await _repository.DefinirStageOutAsync(idChamada);

            case Rotina.DefinePosicaoNaExpedicao:
                return await _repository.DefinirExpedicaoAsync(idChamada);

            case Rotina.DesalocaEndereco:
                return await _repository.DesalocarEnderecoAsync(idChamada);

            case Rotina.LeDocaDeExpedicao:
                return await _repository.LeituraDocaAsync(idChamada);

            case Rotina.SempreOkNenhumaValidacao:
                return await _repository.SempreOkAsync(idChamada);

            case Rotina.LeDocaDeExpedicaoParaOrdemInterna:
                return await _repository.LeituraDocaOrdemInternaAsync(idChamada);

            case Rotina.LeBufferParaOrdemDeTransferencia:
                return await _repository.LeituraBufferOrdemTransferenciaAsync(idChamada);

            default:
                throw new ArgumentOutOfRangeException(nameof(rotina), rotina, "Rotina inválida");
        }
    }
}