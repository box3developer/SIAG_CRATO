using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAG.CrossCutting.Utils
{
    public static class SqlUtil
    {
        public static string GetStringTratadaWhere(string? dados)
        {
            if (string.IsNullOrWhiteSpace(dados))
                return "";

            return "%" + dados.ToLower().Replace(" ", "%") + "%";
        }
    }
}
