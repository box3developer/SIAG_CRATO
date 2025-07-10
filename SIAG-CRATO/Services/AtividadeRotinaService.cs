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

            case Rotina.ValidaEAtribuiPalletNoEnderecoDestino:
                {
                    _ = await _repository.VerificaDesalocaPalletOrigemAsync(idChamada);
                    var palletStageIn = await _repository.AlocarPalletEnderecoAsync(idChamada);

                    return palletStageIn;
                }

            case Rotina.VerificaPalletOrigemERemoveAlocacao:
                return await _repository.LeituraAposCheioAsync(idChamada);

            case Rotina.AtribuiPalletNoEnderecoOrigem:
                return await _repository.AlocarPalletLivreAsync(idChamada);

            case Rotina.LePalletDoBuffer:
                return await _repository.LeituraAposCheioAsync(idChamada);

            case Rotina.LeStageIn:
                return await _repository.LeituraStageInAsync(idChamada);

            case Rotina.ArmazenaPallet:
                return await _repository.ArmazenarPalletAsync(idChamada);

            case Rotina.SempreOkNenhumaValidacao:
                return await _repository.SempreOkAsync(idChamada);

            default:
                throw new ArgumentOutOfRangeException(nameof(rotina), rotina, "Rotina inválida");
        }
    }
}