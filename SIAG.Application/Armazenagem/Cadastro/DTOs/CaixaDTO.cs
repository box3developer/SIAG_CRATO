using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    public class CaixaDTO
    {
        public string CaixaId { get; set; }

        public Guid AgrupadorId { get; set; }

        public AgrupadorAtivoDTO Agrupador { get; set; }

        public int PalletId { get; set; }

        public int ProgramaId { get; set; }

        public int PedidoId { get; set; }

        public string CdProduto { get; set; }

        public string CdCor { get; set; }

        public string CdGrudeTamanho { get; set;}

        public int NrCaixa { get; set; }

        public int NrPares { get; set; }

        public bool FgRfid { get; set; }

        public int FgStatus { get; set; }

        public DateTime DtEmbalagem { get; set; }

        public DateTime DtSorter { get; set; }

        public DateTime DtEstufamento { get; set; }

        public DateTime DtExpedicao { get; set; }

        public decimal QtPeso { get; set; }
    }
}
