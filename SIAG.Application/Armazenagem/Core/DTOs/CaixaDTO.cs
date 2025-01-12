using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SIAG.Application.Armazenagem.Core.DTOs
{
    public class CaixaDTO
    {
        public string IdCaixa { get; set; }

        public Guid IdAgrupador { get; set; }

        public AgrupadorAtivoDTO Agrupador { get; set; }

        public int IdPallet { get; set; }

        public int IdPrograma { get; set; }

        public int IdPedido { get; set; }

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
