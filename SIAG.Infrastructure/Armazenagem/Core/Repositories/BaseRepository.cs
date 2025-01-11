using Dapper;
using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Infrastructure.Armazenagem.Core.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

namespace SIAG.Infrastructure.Armazenagem.Core.Repositorios
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly IDbConnection _connection;
        private readonly string _tableName;
        private readonly string _keyColumn;

        public BaseRepository(IDbConnection connection)
        {
            _connection = connection;
            _tableName = GetTableName();
            _keyColumn = GetKeyColumn();
        }

        private string GetKeyColumn()
        {
            var keyProperty = typeof(TEntity).GetProperties()
                                             .FirstOrDefault(p => p.GetCustomAttribute<KeyAttribute>() != null);

            if (keyProperty == null)
                throw new InvalidOperationException($"No [Key] attribute found in {typeof(TEntity).Name}");

            return keyProperty.Name;
        }

        private string GetTableName()
        {
            var tableAttribute = typeof(TEntity).GetCustomAttribute<TableAttribute>();
            if (tableAttribute != null && !string.IsNullOrWhiteSpace(tableAttribute.Name))
            {
                return tableAttribute.Name;
            }

            // Fallback para o nome da classe
            return typeof(TEntity).Name;
        }

        public async Task<TEntity?> GetByIdAsync(dynamic id)
        {
            var sqlBuilder = new SqlBuilder();
            var sql = sqlBuilder.BuildSelectById(_tableName, _keyColumn);
            return await _connection.QuerySingleOrDefaultAsync<TEntity>(sql, new { Id = id });
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var sqlBuilder = new SqlBuilder();
            var sql = sqlBuilder.BuildSelect<TEntity>(_tableName);
            return await _connection.QueryAsync<TEntity>(sql);
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            var sqlBuilder = new SqlBuilder();
            var sql = sqlBuilder.BuildInsert<TEntity>(_tableName);
            return await _connection.ExecuteAsync(sql, entity);
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            var sqlBuilder = new SqlBuilder();
            var sql = sqlBuilder.BuildUpdate<TEntity>(_tableName, _keyColumn);
            return await _connection.ExecuteAsync(sql, entity);
        }

        public async Task<int> DeleteAsync(dynamic id)
        {
            var sqlBuilder = new SqlBuilder();
            var sql = sqlBuilder.BuildDelete(_tableName, _keyColumn);
            return await _connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
