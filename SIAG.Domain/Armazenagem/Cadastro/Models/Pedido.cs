using SIAG.Domain.Armazenagem.Cadastro.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [CustomKeyEntity]
    [Table("pedido")]
    public class Pedido
    {
        [Key]
        [Column("id_pedido")]
        public int IdPedido { get; set; }

        [Column("id_transportadora")]
        public int? IdTransportadora { get; set; }

        [Column("cd_pedido")]
        public int? CdPedido { get; set; }

        [Column("cd_lote")]
        public int? CdLote { get; set; }

        [Column("cd_box")]
        public int? CdBox { get; set; }

        [Column("cd_cliente")]
        public int? CdCliente { get; set; }

        [Column("cd_estabelecimento")]
        public string? CdEstabelecimento { get; set; } = string.Empty;

        [Column("id_notafiscal")]
        public int? IdNotaFiscal { get; set; }

        [Column("cd_canal")]
        public int? CdCanal { get; set; }

        [Column("cd_ordemexportacao")]
        public int? CdOrdemExportacao { get; set; }

        [Column("cd_veiculoexportacao")]
        public int? CdVeiculoExportacao { get; set; }

        [Column("tp_carga")]
        public string? TpCarga { get; set; } = string.Empty;

        [Column("tp_cargaaglutinado")]
        public string? TpCargaAglutinado { get; set; } = string.Empty;

        [Column("cd_rota")]
        public string? CdRota { get; set; } = string.Empty;

        [Column("qt_caixa")]
        public int? QtCaixa { get; set; }

        [Column("qt_cubagemcaixa")]
        public decimal? QtCubagemCaixa { get; set; }

        [Column("qt_acessorio")]
        public int? QtAcessorio { get; set; }

        [Column("qt_cubagemacessorio")]
        public decimal? QtCubagemAcessorio { get; set; }

        [Column("qt_display")]
        public int? QtDisplay { get; set; }

        [Column("qt_cubagemdisplay")]
        public decimal? QtCubagemDisplay { get; set; }

        [Column("qt_expositores")]
        public int? QtExpositores { get; set; }

        [Column("qt_cubagemexpositores")]
        public decimal? QtCubagemExpositores { get; set; }

        [Column("fg_status")]
        public int? FgStatus { get; set; }

        [Column("dt_importacao")]
        public DateTime? DtImportacao { get; set; }

        [Column("cd_ordemexportacaodefinitiva")]
        public long? CdOrdemExportacaoDefinitiva { get; set; }

        [Column("cd_veiculoexportacaodefinitiva")]
        public int? CdVeiculoExportacaoDefinitiva { get; set; }

        [Column("dt_previsaoexportacao")]
        public DateTime? DtPrevisaoExportacao { get; set; }

        [Column("dt_implantacao")]
        public DateTime? DtImplantacao { get; set; }

        [Column("dt_atualizacao")]
        public DateTime? DtAtualizacao { get; set; }

        [Column("dt_predata")]
        public DateTime? DtPredata { get; set; }

        [Column("fg_sku")]
        public int? FgSku { get; set; }

        [Column("cd_sequenciaexpedicao")]
        public int? CdSequenciaExpedicao { get; set; }
    }
}
