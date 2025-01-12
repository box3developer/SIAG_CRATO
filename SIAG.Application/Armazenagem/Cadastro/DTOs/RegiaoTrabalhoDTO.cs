namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class RegiaoTrabalhoDTO
    {
        public int IdRegiaoTrabalho { get; set; }

        public int IdDeposito { get; set; }

        public DepositoDTO? Deposito { get; set; }

        public string NmRegiaoTrabalho { get; set; } = string.Empty;
    }
}
