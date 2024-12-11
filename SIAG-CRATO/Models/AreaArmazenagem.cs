using SIAG_CRATO.Data;

namespace SIAG_CRATO.Models
{
    public class AreaArmazenagem
    {
        public long Codigo { get; set; }
        public TipoAreaModel TipoArea { get; set; } = new TipoAreaModel();
        public EnderecoModel Endereco { get; set; }
        public AgrupadorAtivoModel Agrupador { get; set; }
        public int PosicaoX { get; set; }
        public int PosicaoY { get; set; }
        public int Lado { get; set; }
        public StatusAreaArmazenagem Status { get; set; }
        public string Identificacao { get; set; }
    }
}
