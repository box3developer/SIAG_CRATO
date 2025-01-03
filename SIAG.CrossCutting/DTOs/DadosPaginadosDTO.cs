using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAG.CrossCutting.DTOs
{

    public class DadosPaginadosDTO<T>
    {
        public DadosPaginadosDTO()
        {
        }

        public List<T> Dados { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalRegisters { get; set; }
    }
}
