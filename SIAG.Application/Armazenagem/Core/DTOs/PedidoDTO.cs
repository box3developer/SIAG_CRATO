namespace SIAG.Application.Armazenagem.Core.DTOs
{
    public class PedidoDTO
    {
        public int IdPedido { get; set; }

        public int? IdTransportadora { get; set; }

        public int? CdPedido { get; set; }

        public int? CdLote { get; set; }

        public int? CdBox { get; set; }

        public int? CdCliente { get; set; }

        public string? CdEstabelecimento { get; set; } = string.Empty;

        public int? IdNotaFiscal { get; set; }

        public int? CdCanal { get; set; }

        public int? CdOrdemExportacao { get; set; }

        public int? CdVeiculoExportacao { get; set; }

        public string? TpCarga { get; set; } = string.Empty;

        public string? TpCargaAglutinado { get; set; } = string.Empty;

        public string? CdRota { get; set; } = string.Empty;

        public int? QtCaixa { get; set; }

        public decimal? QtCubagemCaixa { get; set; }

        public int? QtAcessorio { get; set; }

        public decimal? QtCubagemAcessorio { get; set; }

        public int? QtDisplay { get; set; }

        public decimal? QtCubagemDisplay { get; set; }

        public int? QtExpositores { get; set; }

        public decimal? QtCubagemExpositores { get; set; }

        public int? FgStatus { get; set; }

        public DateTime? DtImportacao { get; set; }

        public long? CdOrdemExportacaoDefinitiva { get; set; }

        public int? CdVeiculoExportacaoDefinitiva { get; set; }

        public DateTime? DtPrevisaoExportacao { get; set; }

        public DateTime? DtImplantacao { get; set; }

        public DateTime? DtAtualizacao { get; set; }

        public DateTime? DtPredata { get; set; }

        public int? FgSku { get; set; }

        public int? CdSequenciaExpedicao { get; set; }
    }
}
