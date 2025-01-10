using SIAG.CrossCutting.Utils;

namespace SIAG.CrossCutting.DTOs
{
    public class StatusDynamicService
    {
        public static List<StatusDTO> ObterStatusPorNome(string nomeClasse)
        {
            const string namespaceBase = "SIAG.CrossCutting.Status";

            // Obter todos os tipos no namespace especificado
            var tiposNoNamespace = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.Namespace == namespaceBase)
                .ToList();

            // 1. Busca Exata (Case-Sensitive)
            var tipoClasse = tiposNoNamespace
                .FirstOrDefault(t => t.Name == nomeClasse);

            // 2. Busca Exata (Case-Insensitive)
            if (tipoClasse == null)
            {
                tipoClasse = tiposNoNamespace
                    .FirstOrDefault(t => string.Equals(t.Name, nomeClasse, StringComparison.OrdinalIgnoreCase));
            }

            // 3. Busca Parcial
            if (tipoClasse == null)
            {
                tipoClasse = tiposNoNamespace
                    .FirstOrDefault(t => t.Name.Contains(nomeClasse, StringComparison.OrdinalIgnoreCase));
            }

            // 4. Se nenhuma correspondência for encontrada, lança exceção
            if (tipoClasse == null)
            {
                throw new ArgumentException($"Classe similar a '{nomeClasse}' não encontrada.");
            }

            // Retorna a lista de status utilizando o StatusUtils
            return StatusUtils.GetStatusList(tipoClasse);
        }

    }
}
