namespace SIAG.Domain.Armazenagem.Core.Models
{
    public class DesempenhoOnlineDTO
    {
        public int IdDesempenhoonline { get; set; }

        public int IdOperador { get; set; }

        public int IdEquipamentomodelo { get; set; }

        public int IdSetortrabalho { get; set; }

        public int NrTempologado { get; set; }

        public int NrTempoprevisto { get; set; }

        public int NrTemporealizado { get; set; }

        public int QtPrevista { get; set; }

        public int QtRealizada { get; set; }

        public DateTime DtRegistro { get; set; }
    }
}
