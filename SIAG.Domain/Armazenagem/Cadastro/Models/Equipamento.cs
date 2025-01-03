using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    public class Equipamento
    {
        public int EquipamentoId { get; set; }

        public int EquipamentoModeloId { get; set; }

        public int SetorTrabalhoId { get; set; }

        public int OperadorId { get; set; }

        public string NmEquipamento { get; set; }

        public string NmIdentificador { get; set; }

        public int FgStatus { get; set; }

        public DateTime DtInclusao { get; set; }

        public DateTime DtManutencao { get; set; }

        public string NmObservacao { get; set; }

        public string CdUltimaLeitura { get; set; }

        public DateTime DtUltimaLeitura { get; set; }

        public int EnderecoId { get; set; }

        public string NmIp { get; set; }

        public int FgStatusTrocaCaracol { get; set; }

        public string NmAbreviadoEquipamento { get; set; }

        public string CdLeituraPendente { get; set; }

        public string NmUsuarioLiberacao { get; set; }
    }
}
