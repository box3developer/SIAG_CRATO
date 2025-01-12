using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SIAG.Application.Armazenagem.Core.DTOs
{
    public class EquipamentoDTO
    {
        public int IdEquipamento { get; set; }

        public int IdEquipamentoModelo { get; set; }

        public int IdSetorTrabalho { get; set; }

        public int IdOperador { get; set; }

        public string NmEquipamento { get; set; }

        public string NmIdentificador { get; set; }

        public int FgStatus { get; set; }

        public DateTime DtInclusao { get; set; }

        public DateTime DtManutencao { get; set; }

        public string NmObservacao { get; set; }

        public string CdUltimaLeitura { get; set; }

        public DateTime DtUltimaLeitura { get; set; }

        public int IdEndereco { get; set; }

        public string NmIp { get; set; }

        public int FgStatusTrocaCaracol { get; set; }

        public string NmAbreviadoEquipamento { get; set; }

        public string CdLeituraPendente { get; set; }

        public string NmUsuarioLiberacao { get; set; }
    }
}
