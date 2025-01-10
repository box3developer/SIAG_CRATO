using Dapper;
using SIAG.Domain.Armazenagem.Core.Interfaces;
using SIAG.Infrastructure.Armazenagem.Core.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace SIAG.Infrastructure.Armazenagem.Core.Repositorios
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        private readonly IDbConnection _connection;
        private readonly string _tableName;

        public BaseRepository(IDbConnection connection, string tableName)
        {
            _connection = connection;
            _tableName = tableName;
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            var keyColumn = GetKeyColumn();
            var sqlBuilder = new SqlBuilder();
            var sql = sqlBuilder.BuildSelectById(_tableName, keyColumn);
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
            var keyColumn = GetKeyColumn();
            var sqlBuilder = new SqlBuilder();
            var sql = sqlBuilder.BuildUpdate<TEntity>(_tableName, keyColumn);
            return await _connection.ExecuteAsync(sql, entity);
        }

        public async Task<int> DeleteAsync(TKey id)
        {
            var keyColumn = GetKeyColumn();
            var sqlBuilder = new SqlBuilder();
            var sql = sqlBuilder.BuildDelete(_tableName, keyColumn);
            return await _connection.ExecuteAsync(sql, new { Id = id });
        }

        private string GetKeyColumn()
        {
            var keyProperty = typeof(TEntity).GetProperties()
                                             .FirstOrDefault(p => p.GetCustomAttribute<KeyAttribute>() != null);

            if (keyProperty == null)
                throw new InvalidOperationException($"No [Key] attribute found in {typeof(TEntity).Name}");

            return keyProperty.Name;
        }
    }
}
