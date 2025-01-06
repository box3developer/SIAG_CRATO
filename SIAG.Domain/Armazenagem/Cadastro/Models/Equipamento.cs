using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("equipamento")]

    public class Equipamento
    {
        [Key]
        [Column("id_equipamento")]
        public int IdEquipamento { get; set; }

        [Column("id_equipamento_modelo")]
        public int IdEquipamentoModelo { get; set; }

        [Column("id_setor_trabalho")]
        public int IdSetorTrabalho { get; set; }

        [Column("id_operador")]
        public int IdOperador { get; set; }

        [Column("nm_equipamento")]
        public string NmEquipamento { get; set; } = string.Empty;

        [Column("nm_identificador")]
        public string NmIdentificador { get; set; } = string.Empty;

        [Column("fg_status")]
        public int FgStatus { get; set; }

        [Column("dt_inclusao")]
        public DateTime DtInclusao { get; set; }

        [Column("dt_manutencao")]
        public DateTime DtManutencao { get; set; }

        [Column("nm_observacao")]
        public string NmObservacao { get; set; } = string.Empty;

        [Column("cd_ultima_leitura")]
        public string CdUltimaLeitura { get; set; } = string.Empty;

        [Column("dt_ultima_leitura")]
        public DateTime DtUltimaLeitura { get; set; }

        [Column("endereco_id")]
        public int EnderecoId { get; set; }

        [Column("nm_ip")]
        public string NmIp { get; set; } = string.Empty;

        [Column("fg_status_troca_caracol")]
        public int FgStatusTrocaCaracol { get; set; }

        [Column("nm_abreviado_equipamento")]
        public string NmAbreviadoEquipamento { get; set; } = string.Empty;

        [Column("cd_leitura_pendente")]
        public string CdLeituraPendente { get; set; } = string.Empty;

        [Column("nm_usuario_liberacao")]
        public string NmUsuarioLiberacao { get; set; } = string.Empty;
    }
}
