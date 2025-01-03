using SIAG.CrossCutting.DTOs;
using SIAG.Domain.Armazenagem.Cadastro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAG.Domain.Armazenagem.Cadastro.Interfaces
{
    public interface IAreaArmazenagemRepository : IBaseRepository<AreaArmazenagem, int>
    {
        public Task<List<AreaArmazenagem>> GetListAsync(FiltroPaginacaoDTO dto);
    }
}
