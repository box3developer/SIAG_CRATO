using SIAG.Domain.Armazenagem.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Application.Armazenagem.Cadastro.DTOs
{
    public class AreaArmazenagemDTO
    {
        public long IdAreaArmazenagem { get; set; }

        public int IdTipoArea { get; set; }

        public TipoAreaDTO? TipoArea { get; set; }


        public int IdEndereco { get; set; }

        public EnderecoDTO? Endereco { get; set; }


        public Guid? IdAgrupador { get; set; }

        public AgrupadorAtivoDTO? Agrupador { get; set; }

        public int NrPosicaoX { get; set; }

        public int NrPosicaoY { get; set; }

        public int? NrLado { get; set; }

        public int FgStatus { get; set; }

        public string? CdIdentificacao { get; set; } = string.Empty;


        public Guid? IdAgrupadorReservado { get; set; }

        public AgrupadorAtivoDTO? AgrupadorReservado { get; set; }
    }
}
