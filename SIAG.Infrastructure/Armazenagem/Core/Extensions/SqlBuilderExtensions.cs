using Dapper;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SIAG.Infrastructure.Armazenagem.Core.Extensions
{
    public static class SqlBuilderExtensions
    {
        public static string BuildInsert<TEntity>(this SqlBuilder builder, string tableName)
        {
            var properties = typeof(TEntity).GetProperties()
                                            .Where(p => p.GetCustomAttribute<KeyAttribute>() == null);

            var columns = string.Join(", ", properties.Select(p => p.Name));
            var values = string.Join(", ", properties.Select(p => "@" + p.Name));

            return $"INSERT INTO {tableName} ({columns}) VALUES ({values});";
        }

        public static string BuildUpdate<TEntity>(this SqlBuilder builder, string tableName, string keyColumn)
        {
            var properties = typeof(TEntity).GetProperties()
                                            .Where(p => p.Name != keyColumn);

            var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));

            return $"UPDATE {tableName} SET {setClause} WHERE {keyColumn} = @{keyColumn};";
        }

        public static string BuildDelete(this SqlBuilder builder, string tableName, string keyColumn)
        {
            return $"DELETE FROM {tableName} WHERE {keyColumn} = @{keyColumn};";
        }

        public static string BuildSelect<TEntity>(this SqlBuilder builder, string tableName, string[] columns = null)
        {
            if (columns == null || columns.Length == 0)
            {
                columns = typeof(TEntity).GetProperties().Select(p => p.Name).ToArray();
            }

            var columnsClause = string.Join(", ", columns);

            return $"SELECT {columnsClause} FROM {tableName};";
        }

        public static string BuildSelectById(this SqlBuilder builder, string tableName, string keyColumn)
        {
            return $"SELECT * FROM {tableName} WHERE {keyColumn} = @{keyColumn};";
        }

        public static string BuildDynamicWhere<TEntity>(this SqlBuilder builder, object filters)
        {
            var properties = filters.GetType().GetProperties()
                                    .Where(p => p.GetValue(filters) != null);

            var whereClause = string.Join(" AND ", properties.Select(p => $"{p.Name} = @{p.Name}"));

            return whereClause;
        }
    }
}
