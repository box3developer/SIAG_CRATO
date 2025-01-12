namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class SetorTrabalhoDTO
    {
        public int IdSetorTrabalho { get; set; }

        public int IdDeposito { get; set; }

        public DepositoDTO? Deposito { get; set; }

        public string? NmSetorTrabalho { get; set; }
    }
}
