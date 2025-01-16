using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [CustomKeyEntity]
    [Table("tempoatividade")]
    public class TempoAtividade
    {
        [Key]
        [Column("id_tempoatividade")]
        public int IdTempoAtividade { get; set; }

        [Column("id_equipamentomodelo")]
        public int? IdEquipamentoModelo { get; set; }

        [Column("id_setortrabalho")]
        public int? IdSetorTrabalho { get; set; }

        [Column("nr_tempooperacao")]
        public decimal? NrTempoOperacao { get; set; }

        [Column("nr_tempodeslocamento")]
        public decimal? NrTempoDeslocamento { get; set; }

        [Column("nr_tempocoluna")]
        public decimal? NrTempoColuna { get; set; }

        [Column("nr_tempocorredor")]
        public decimal? NrTempoCorredor { get; set; }

        [Column("nr_tempoaltura")]
        public decimal? NrTempoAltura { get; set; }

        [Column("nr_posicao1")]
        public decimal? NrPosicao1 { get; set; }

        [Column("nr_posicao2")]
        public decimal? NrPosicao2 { get; set; }

        [Column("nr_posicao3")]
        public decimal? NrPosicao3 { get; set; }

        [Column("nr_posicao4")]
        public decimal? NrPosicao4 { get; set; }

        [Column("nr_posicao5")]
        public decimal? NrPosicao5 { get; set; }

        [Column("nr_posicao6")]
        public decimal? NrPosicao6 { get; set; }

        [Column("nr_posicao7")]
        public decimal? NrPosicao7 { get; set; }

        [Column("nr_posicao8")]
        public decimal? NrPosicao8 { get; set; }

        [Column("nr_posicao9")]
        public decimal? NrPosicao9 { get; set; }

        [Column("nr_posicao10")]
        public decimal? NrPosicao10 { get; set; }

        [Column("nr_posicao11")]
        public decimal? NrPosicao11 { get; set; }

        [Column("nr_posicao12")]
        public decimal? NrPosicao12 { get; set; }

        [Column("nr_posicao13")]
        public decimal? NrPosicao13 { get; set; }

        [Column("nr_posicao14")]
        public decimal? NrPosicao14 { get; set; }

        [Column("nr_posicao15")]
        public decimal? NrPosicao15 { get; set; }

        [Column("nr_posicao16")]
        public decimal? NrPosicao16 { get; set; }

        [Column("nr_posicao17")]
        public decimal? NrPosicao17 { get; set; }

        [Column("nr_posicao18")]
        public decimal? NrPosicao18 { get; set; }

        [Column("nr_posicao19")]
        public decimal? NrPosicao19 { get; set; }

        [Column("nr_posicao20")]
        public decimal? NrPosicao20 { get; set; }

        [Column("nr_posicao21")]
        public decimal? NrPosicao21 { get; set; }

        [Column("nr_posicao22")]
        public decimal? NrPosicao22 { get; set; }

        [Column("nr_percentualineficiencia")]
        public decimal? NrPercentualIneficiencia { get; set; }

        [Column("id_atividade")]
        public int? IdAtividade { get; set; }
    }
}
