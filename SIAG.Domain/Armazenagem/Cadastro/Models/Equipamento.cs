using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [BasicEntity]
    [Table("equipamento")]
    public class Equipamento
    {
        [Key]
        [Column("id_equipamento")]
        public int IdEquipamento { get; set; }


        [Column("id_equipamentomodelo")]
        public int? IdEquipamentoModelo { get; set; }

        [ForeignKey(nameof(IdEquipamentoModelo))]
        public EquipamentoModelo? EquipamentoModelo { get; set; }


        [Column("id_setortrabalho")]
        public int? IdSetorTrabalho { get; set; }
        
        [ForeignKey(nameof(IdSetorTrabalho))]
        public SetorTrabalho? SetorTrabalho { get; set; }


        [Column("id_operador")]
        public long? IdOperador { get; set; }

        [ForeignKey(nameof(IdOperador))]
        public Operador? Operador { get; set; }


        [Column("id_operador2")]
        public long? IdOperador2 { get; set; }

        [ForeignKey(nameof(IdOperador2))]
        public Operador? Operador2 { get; set; }


        [Column("id_endereco")]
        public int? IdEndereco { get; set; }

        [ForeignKey(nameof(IdEndereco))]
        public Endereco? Endereco { get; set; }


        [Column("nm_equipamento")]
        public string? NmEquipamento { get; set; } = string.Empty;

        [Column("nm_identificador")]
        public string? NmIdentificador { get; set; } = string.Empty;

        [Column("fg_status")]
        public int? FgStatus { get; set; }

        [Column("dt_inclusao")]
        public DateTime? DtInclusao { get; set; }

        [Column("dt_manutencao")]
        public DateTime? DtManutencao { get; set; }

        [Column("nm_observacao")]
        public string? NmObservacao { get; set; } = string.Empty;

        [Column("cd_ultimaleitura")]
        public string? CdUltimaLeitura { get; set; } = string.Empty;

        [Column("dt_ultimaleitura")]
        public DateTime? DtUltimaLeitura { get; set; }

        [Column("nm_ip")]
        public string? NmIp { get; set; } = string.Empty;

        [Column("fg_statustrocacaracol")]
        public int? FgStatusTrocaCaracol { get; set; }

        [Column("nm_abreviado_equipamento")]
        public string? NmAbreviadoEquipamento { get; set; } = string.Empty;

        [Column("cd_leitura_pendente")]
        public string? CdLeituraPendente { get; set; } = string.Empty;

        [Column("nm_usuario_liberacao")]
        public string? NmUsuarioLiberacao { get; set; } = string.Empty;
    }
}
