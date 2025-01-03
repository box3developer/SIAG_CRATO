using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAG.CrossCutting.DTOs
{
    public class SelectDTO<T>
    {
        public T Id { get; set; }
        public string? Descricao { get; set; } = string.Empty;
    }
}
