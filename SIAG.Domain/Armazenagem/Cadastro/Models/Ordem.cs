using SIAG.Domain.Armazenagem.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [BasicEntity]
    [Table("ordem")]
    public class Ordem
    {
        [Key]
        [Column("id_transportadora")]
        public int IdTransportadora { get; set; }

        [Column("id_veiculo")]
        public int IdVeiculo { get; set; }

        [Column("id_motorista")]
        public int IdMotorista { get; set; }

        [Column("id_endereco")]
        public int IdEndereco { get; set; }

        [Column("qt_cubagem")]
        public decimal QtCubagem { get; set; }

        [Column("dt_geracao")]
        public DateTime DtGeracao { get; set; }

        [Column("dt_previsao")]
        public DateTime DtPrevisao { get; set; }

        [Column("dt_entrada")]
        public DateTime DtEntrada { get; set; }

        [Column("dt_inicio")]
        public DateTime DtInicio { get; set; }

        [Column("dt_fim")]
        public DateTime DtFim { get; set; }

        [Column("dt_saida")]
        public DateTime DtSaida { get; set; }

        [Column("fg_controleendereco")]
        public int FgControleendereco { get; set; }

        [Column("fg_controlesms")]
        public int FgControlesms { get; set; }

        [Column("id_motoristamanobrista")]
        public int IdMotoristamanobrista { get; set; }

        [Column("cd_centrocusto")]
        public string CdCentrocusto { get; set; }

        [Column("nr_custovalor")]
        public decimal NrCustovalor { get; set; }

        [Column("id_solicitante")]
        public int IdSolicitante { get; set; }

        [Column("id_motivo")]
        public int IdMotivo { get; set; }

        [Column("priorizar")]
        public bool Priorizar { get; set; }
    }
}
