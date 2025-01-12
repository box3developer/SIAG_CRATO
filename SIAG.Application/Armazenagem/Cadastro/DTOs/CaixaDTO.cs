namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class CaixaDTO
    {
        public string IdCaixa { get; set; } = string.Empty;


        public Guid? IdAgrupador { get; set; }

        public AgrupadorAtivoDTO? Agrupador { get; set; }


        public int? IdPallet { get; set; }

        public PalletDTO? Pallet { get; set; }


        public int? IdPrograma { get; set; }

        public ProgramaDTO? Programa { get; set; }


        public int? IdPedido { get; set; }

        public PedidoDTO? Pedido { get; set; }


        public string? CdProduto { get; set; } = string.Empty;

        public string? CdCor { get; set; } = string.Empty;

        public string? CdGradeTamanho { get; set; } = string.Empty;

        public int? NrCaixa { get; set; }

        public int? NrPares { get; set; }

        public bool? IdFgRf { get; set; }

        public int? FgStatus { get; set; }

        public DateTime? DtEmbalagem { get; set; }

        public DateTime? DtSorter { get; set; }

        public DateTime? DtEstufamento { get; set; }

        public DateTime? DtExpedicao { get; set; }

        public decimal? QtPeso { get; set; }
    }
}
