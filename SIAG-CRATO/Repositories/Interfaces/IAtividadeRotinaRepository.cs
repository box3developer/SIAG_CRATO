using SIAG_CRATO.Services;

namespace SIAG_CRATO.Repositories.Interfaces
{
    public interface IAtividadeRotinaRepository
    {
        public Task<ValidacaoEnderecoResult> AlocarPalletAsync(Guid idChamada);
        public Task<ValidacaoEnderecoResult> AlocarPalletEnderecoAsync(Guid idChamada);
        public Task<ValidacaoEnderecoResult> AlocarPalletLivreAsync(Guid idChamada);
        public Task<ValidacaoEnderecoResult> ArmazenarPalletAsync(Guid idChamada);
        public Task<ValidacaoEnderecoResult> DefinirExpedicaoAsync(Guid idChamada);
        public Task<ValidacaoEnderecoResult> DefinirStageOutAsync(Guid idChamada);
        public Task<ValidacaoEnderecoResult> DesalocarEnderecoAsync(Guid idChamada);
        public Task<ValidacaoEnderecoResult> LeituraBufferOrdemTransferenciaAsync(Guid idChamada);
        public Task<ValidacaoEnderecoResult> LeituraBufferAsync(Guid idChamada);
        public Task<ValidacaoEnderecoResult> LeituraDocaOrdemInternaAsync(Guid idChamada);
        public Task<ValidacaoEnderecoResult> LeituraDocaAsync(Guid idChamada);
        public Task<ValidacaoEnderecoResult> LeituraStageInAsync(Guid idChamada);
        public Task<ValidacaoEnderecoResult> SempreOkAsync(Guid idChamada);
        public Task<ValidacaoEnderecoResult> ValidarEnderecoExatoOrigemAsync(Guid idChamada);
        public Task<ValidacaoEnderecoResult> VerificaDesalocaPalletOrigemAsync(Guid idChamada);
    }
}
