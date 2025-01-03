using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAG.CrossCutting.DTOs
{
    public class FiltroPaginacaoDTO
    {
        public string Pesquisa { get; set; } = string.Empty;
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public bool? Ativo { get; set; }
        public bool Impressao { get; set; }
    }
}
