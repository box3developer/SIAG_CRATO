namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class CaixaLeituraDTO
    {
        public long IdCaixaleitura { get; set; }
        public DateTime? DtLeitura { get; set; }
        public bool? FgCancelado { get; set; }

        public int? FgStatus { get; set; }

        public int? FgTipo { get; set; }


        public long? IdAreaarmazenagem { get; set; }

        public AreaArmazenagemDTO? Areaarmazenagem { get; set; }


        public string? IdCaixa { get; set; }

        public CaixaDTO? Caixa { get; set; }


        public int? IdEndereco { get; set; }

        public EnderecoDTO? Endereco { get; set; }


        public int? IdEquipamento { get; set; }

        public EquipamentoDTO? Equipamento { get; set; }


        public long? IdOperador { get; set; }

       public OperadorDTO? Operador { get; set; }


        public long? IdOrdem { get; set; }

        public int? IdPallet { get; set; }

        public PalletDTO? Pallet { get; set; }
    }
}
