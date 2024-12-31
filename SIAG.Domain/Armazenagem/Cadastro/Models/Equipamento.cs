using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    [Table("equipamento")]
    public class Equipamento
    {
        [Key]
        [Column("id_equipamento")]
        public int EquipamentoId { get; set; }

        [Column("id_equipamentomodelo")]
        public int EquipamentoModeloId { get; set; }

        [Column("id_setortrabalho")]
        public int SetorTrabalhoId { get; set; }

        [Column("id_operador")]
        public int OperadorId { get; set; }

        [Column("nm_equipamento")]
        public string NmEquipamento { get; set; }

        [Column("nm_identificador")]
        public string NmIdentificador { get; set; }

        [Column("gf_status")]
        public int FgStatus { get; set; }

        [Column("dt_inclusao")]
        public DateTime DtInclusao { get; set; }

        [Column("dt_manutencao")]
        public DateTime DtManutencao { get; set; }

        [Column("nm_observacao")]
        public string NmObservacao { get; set; }

        [Column("cd_ultimaleitura")]
        public string CdUltimaLeitura { get; set; }

        [Column("dt_ultimaleitura")]
        public DateTime DtUltimaLeitura { get; set; }

        [Column("id_endereco")]
        public int EnderecoId { get; set; }

        [Column("nm_ip")]
        public string NmIp { get; set; }

        [Column("fg_statustrocacaracol")]
        public int FgStatusTrocaCaracol { get; set; }

        [Column("nm_abreviado_equipamento")]
        public string NmAbreviadoEquipamento { get; set; }

        [Column("cd_leitura_pendente")]
        public string CdLeituraPendente { get; set; }

        [Column("nm_usuario_liberacao")]
        public string NmUsuarioLiberacao { get; set; }
    }
}
