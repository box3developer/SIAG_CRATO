using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAG.CrossCutting.Utils
{
    public static class HandleException
    {
        public static void FkException(Exception ex)
        {
            if (ex.InnerException != null && ex.InnerException.ToString().Contains("violates foreign key constraint"))
                throw new ValidacaoException("Não é possivel excluir o item selecionado, pois o mesmo contém vinculo com outros itens.");

            throw ex;
        }
    }
}
}
