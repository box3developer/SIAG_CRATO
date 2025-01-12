using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class ProgramaDTO
    {
        public int IdPrograma { get; set; }

        public int CdPrograma { get; set; }

        public int CdDocumento { get; set; }

        public int CdFabrica { get; set; }

        public int CdEstabelecimento { get; set; }

        public string CdEquipamento { get; set; }

        public DateTime DtLiberacao { get; set; }

        public int FgTipo { get; set; }

        public string CdDeposito { get; set; }

        public decimal QtAlturaCaixa { get; set; }

        public decimal QtLarguraCaixa { get; set; }

        public decimal QtComprimentoCaixa { get; set; }
    }
}
