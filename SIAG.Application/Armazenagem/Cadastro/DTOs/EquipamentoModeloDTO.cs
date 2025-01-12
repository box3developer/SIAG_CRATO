namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class EquipamentoModeloDTO
    {
        public int IdEquipamentoModelo { get; set; }

        public string NmDescricao { get; set; } = string.Empty;

        public int? FgStatus { get; set; }
    }
}
