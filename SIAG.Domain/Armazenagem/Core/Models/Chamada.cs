using SIAG.Domain.Armazenagem.Attributes;
using SIAG.Domain.Armazenagem.Cadastro.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Core.Models
{
    [CustomKeyEntity]
    [Table("chamada")]
    public class Chamada
    {
        [Key]
        [Column("id_chamada")]
        public Guid IdChamada { get; set; }


        [Column("id_palletorigem")]
        public int IdPalletorigem { get; set; }

        [ForeignKey(nameof(IdPalletorigem))]
        public Pallet? Palletorigem { get; set; }


        [Column("id_areaarmazenagemorigem")]
        public long IdAreaarmazenagemorigem { get; set; }

        [ForeignKey(nameof(IdAreaarmazenagemorigem))]
        public AreaArmazenagem? Areaarmazenagemorigem { get; set; }


        [Column("id_palletdestino")]
        public int IdPalletDestino { get; set; }

        [ForeignKey(nameof(IdPalletDestino))]
        public Pallet? PalletDestino { get; set; }


        [Column("id_areaarmazenagemdestino")]
        public long IdAreaarmazenagemdestino { get; set; }

        [ForeignKey(nameof(IdAreaarmazenagemdestino))]
        public AreaArmazenagem? Areaarmazenagemdestino { get; set; }


        [Column("id_palletleitura")]
        public int IdPalletleitura { get; set; }

        [ForeignKey(nameof(IdPalletleitura))]
        public Pallet? Palletleitura { get; set; }


        [Column("id_areaarmazenagemleitura")]
        public long IdAreaarmazenagemleitura { get; set; }

        [ForeignKey(nameof(IdAreaarmazenagemleitura))]
        public AreaArmazenagem? Areaarmazenagemleitura { get; set; }


        [Column("id_operador")]
        public long IdOperador { get; set; }

        [ForeignKey(nameof(IdOperador))]
        public Operador? Operador { get; set; }


        [Column("id_equipamento")]
        public int IdEquipamento { get; set; }

        [ForeignKey(nameof(IdEquipamento))]
        public Equipamento? Equipamento { get; set; }


        [Column("id_atividaderejeicao")]
        public int IdAtividaderejeicao { get; set; }

        [ForeignKey(nameof(IdAtividaderejeicao))]
        public Atividade? Atividaderejeicao { get; set; }


        [Column("id_atividade")]
        public int IdAtividade { get; set; }

        [ForeignKey(nameof(IdAtividade))]
        public Atividade? Atividade { get; set; }


        [Column("fg_status")]
        public int FgStatus { get; set; }

        [Column("dt_chamada")]
        public DateTime DtChamada { get; set; }

        [Column("dt_atendida")]
        public DateTime DtAtendida { get; set; }

        [Column("dt_finalizada")]
        public DateTime DtFinalizada { get; set; }

        [Column("dt_recebida")]
        public DateTime DtRecebida { get; set; }

        [Column("dt_rejeitada")]
        public DateTime DtRejeitada { get; set; }


        [Column("id_chamadaorigem")]
        public Guid IdChamadaorigem { get; set; }

        [ForeignKey(nameof(IdChamadaorigem))]
        public Chamada? Chamadaorigem { get; set; }


        [Column("id_chamadasuspensa")]
        public Guid IdChamadasuspensa { get; set; }

        [ForeignKey(nameof(IdChamadasuspensa))]
        public Chamada? Chamadasuspensa { get; set; }


        [Column("priorizar")]
        public bool Priorizar { get; set; }
    }
}
