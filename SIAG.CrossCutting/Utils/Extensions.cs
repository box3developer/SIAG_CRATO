using SIAG.CrossCutting.DTOs;
using Microsoft.EntityFrameworkCore;

namespace SIAG.CrossCutting.Utils
{
    public static class Extensions
    {
        public static async Task<DadosPaginadosDTO<T>> GetPaged<T>(this IQueryable<T> query, int currentPage, int pageSize, bool impressao) where T : class
        {
            var result = new DadosPaginadosDTO<T>();

            result.CurrentPage = currentPage;
            result.PageSize = pageSize;
            result.TotalRegisters = await query.CountAsync();

            var pageCount = (double)result.TotalRegisters / pageSize;
            result.TotalPages = (int)Math.Ceiling(pageCount);

            var skip = ((int)(currentPage * pageSize));

            if (impressao)
                result.Dados = await query.ToListAsync();
            else
                result.Dados = await query.Skip(skip).Take(pageSize).ToListAsync();


            return result;
        }
    }
}
