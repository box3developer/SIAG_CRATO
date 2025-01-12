namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class OperadorHistoricoDTO
    {
        public long? IdOperador { get; set; }


        public int? CdEvento { get; set; }

        public DateTime? DtEvento { get; set; }


        public int? IdEndereco { get; set; }

        public EnderecoDTO? Endereco { get; set; }


        public int? IdEquipamento { get; set; }

        public EquipamentoDTO? Equipamento { get; set; }
    }
}
