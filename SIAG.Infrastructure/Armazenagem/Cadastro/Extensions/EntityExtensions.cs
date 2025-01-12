using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIAG.Infrastructure.Armazenagem.Cadastro.Extensions
{
    public static class EntityExtensions
    {
        private static DbContext GetDbContextFromQuery<TEntity>(IQueryable<TEntity> query)
    where TEntity : class
        {
            var infrastructure = query.Provider as IInfrastructure<IServiceProvider>;
            var serviceProvider = infrastructure?.Instance;
            var context = serviceProvider?.GetService(typeof(DbContext)) as DbContext;

            if (context == null)
                throw new InvalidOperationException("Não foi possível obter o DbContext da consulta.");

            return context;
        }


        public static IQueryable<TEntity> OrderByPrimaryKey<TEntity>(this IQueryable<TEntity> query)
    where TEntity : class
        {
            // Obtém o provedor de metadados do Entity Framework
            var context = GetDbContextFromQuery(query);
            var entityType = context.Model.FindEntityType(typeof(TEntity));
            var primaryKeyProperty = entityType.FindPrimaryKey()?.Properties.FirstOrDefault();

            if (primaryKeyProperty == null)
                throw new InvalidOperationException($"A chave primária não foi encontrada para o tipo {typeof(TEntity).Name}.");

            var keyName = primaryKeyProperty.Name;

            // Cria a expressão para OrderBy
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var property = Expression.Property(parameter, keyName);
            var lambda = Expression.Lambda(property, parameter);

            return query.Provider.CreateQuery<TEntity>(
                Expression.Call(
                    typeof(Queryable),
                    nameof(Queryable.OrderBy),
                    new Type[] { typeof(TEntity), property.Type },
                    query.Expression,
                    Expression.Quote(lambda)));
        }

        public static IQueryable<TEntity> OrderByPrimaryKeyDescending<TEntity>(this IQueryable<TEntity> query)
    where TEntity : class
        {
            var context = GetDbContextFromQuery(query);
            var entityType = context.Model.FindEntityType(typeof(TEntity));
            var primaryKeyProperty = entityType.FindPrimaryKey()?.Properties.FirstOrDefault();

            if (primaryKeyProperty == null)
                throw new InvalidOperationException($"A chave primária não foi encontrada para o tipo {typeof(TEntity).Name}.");

            var keyName = primaryKeyProperty.Name;

            // Cria a expressão para OrderByDescending
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var property = Expression.Property(parameter, keyName);
            var lambda = Expression.Lambda(property, parameter);

            return query.Provider.CreateQuery<TEntity>(
                Expression.Call(
                    typeof(Queryable),
                    nameof(Queryable.OrderByDescending),
                    new Type[] { typeof(TEntity), property.Type },
                    query.Expression,
                    Expression.Quote(lambda)));
        }
    }
}
