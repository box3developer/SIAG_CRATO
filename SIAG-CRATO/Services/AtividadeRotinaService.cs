using SIAG_CRATO.Repositories.Interfaces;
using SIAG_CRATO.Services;

public class RotinaService
{
    private readonly IAtividadeRotinaRepository _repository;

    public RotinaService(IAtividadeRotinaRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public Task<ValidacaoEnderecoResult> ExecutarRotinaAsync(Rotina rotina, Guid idChamada)
    {
        switch (rotina)
        {
            case Rotina.AlocaPallet:
                return _repository.AlocarPalletAsync(idChamada);

            case Rotina.ValidaEnderecoExatoOrigem:
                return _repository.ValidarEnderecoExatoOrigemAsync(idChamada);

            case Rotina.ValidaEAtribuiPalletNoEnderecoDestino:
                return _repository.AlocarPalletEnderecoAsync(idChamada);

            case Rotina.VerificaPalletOrigemERemoveAlocacao:
                return _repository.VerificaDesalocaPalletOrigemAsync(idChamada);

            case Rotina.AtribuiPalletNoEnderecoOrigem:
                return _repository.AlocarPalletLivreAsync(idChamada);

            case Rotina.LePalletDoBuffer:
                return _repository.LeituraBufferAsync(idChamada);

            case Rotina.LeStageIn:
                return _repository.LeituraStageInAsync(idChamada);

            case Rotina.ArmazenaPallet:
                return _repository.ArmazenarPalletAsync(idChamada);

            case Rotina.DefinePosicaoStageOut:
                return _repository.DefinirStageOutAsync(idChamada);

            case Rotina.DefinePosicaoNaExpedicao:
                return _repository.DefinirExpedicaoAsync(idChamada);

            case Rotina.DesalocaEndereco:
                return _repository.DesalocarEnderecoAsync(idChamada);

            case Rotina.LeDocaDeExpedicao:
                return _repository.LeituraDocaAsync(idChamada);

            case Rotina.SempreOkNenhumaValidacao:
                return _repository.SempreOkAsync(idChamada);

            case Rotina.LeDocaDeExpedicaoParaOrdemInterna:
                return _repository.LeituraDocaOrdemInternaAsync(idChamada);

            case Rotina.LeBufferParaOrdemDeTransferencia:
                return _repository.LeituraBufferOrdemTransferenciaAsync(idChamada);

            default:
                throw new ArgumentOutOfRangeException(nameof(rotina), rotina, "Rotina inválida");
        }
    }
}