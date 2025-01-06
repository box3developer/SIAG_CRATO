using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("chamada")]
    public class Chamada
    {
        [Key]
        [Column("id_chamada")]
        public string IdChamada { get; set; } = string.Empty;

        [Column("dt_atendida")]
        public DateTime DtAtendida { get; set; }

        [Column("dt_chamada")]
        public DateTime DtChamada { get; set; }

        [Column("dt_finalizada")]
        public DateTime DtFinalizada { get; set; }

        [Column("dt_recebida")]
        public DateTime DtRecebida { get; set; }

        [Column("dt_rejeitada")]
        public DateTime DtRejeitada { get; set; }

        [Column("fg_status")]
        public int FgStatus { get; set; }

        [Column("id_areaarmazenagemdestino")]
        public long IdAreaarmazenagemdestino { get; set; }

        [Column("id_areaarmazenagemleitura")]
        public long IdAreaarmazenagemleitura { get; set; }

        [Column("id_areaarmazenagemorigem")]
        public long IdAreaarmazenagemorigem { get; set; }

        [Column("id_atividade")]
        public int IdAtividade { get; set; }

        [Column("id_atividaderejeicao")]
        public int IdAtividaderejeicao { get; set; }


        [Column("id_chamadaorigem")]
        public string IdChamadaorigem { get; set; } = string.Empty;

        [Column("id_chamadasuspensa")]
        public string IdChamadasuspensa { get; set; } = string.Empty;

        [Column("id_equipamento")]
        public int IdEquipamento { get; set; }

        [Column("id_operador")]
        public long IdOperador { get; set; }

        [Column("id_palletdestino")]
        public int IdPalletdestino { get; set; }

        [Column("id_palletleitura")]
        public int IdPalletleitura { get; set; }

        [Column("id_palletorigem")]
        public int IdPalletorigem { get; set; }

    }

}
