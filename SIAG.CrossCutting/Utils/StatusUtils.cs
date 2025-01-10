using SIAG.CrossCutting.DTOs;
using System.Reflection;

namespace SIAG.CrossCutting.Utils
{
    public class StatusUtils
    {
        public static List<StatusDTO> GetStatusList<T>() where T : class
        {
            return GetStatusList(typeof(T));
        }

        public static List<StatusDTO> GetStatusList(Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.IsLiteral && !f.IsInitOnly)
                .Select(f => new StatusDTO
                {
                    Id = (int)(f.GetRawConstantValue() ?? 0),
                    Descricao = f.Name
                })
                .ToList();
        }
    }
}
