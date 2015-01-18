using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Demo.Core.ExtensionMethods
{
    public static class QueryableExtensions
    {
        private static readonly MethodInfo OpenGenericWhereMethod = typeof(Queryable)
            .GetMethods()
            .First(m => "Where".Equals(m.Name, StringComparison.Ordinal));

        public static TQueryable ApplyFilter<TQueryable, TValue>(
            this TQueryable instance,
            string property,
            TValue value) where TQueryable : class, IQueryable
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            if (string.IsNullOrWhiteSpace(property))
            {
                throw new ArgumentException("Property is required", "property");
            }

            Expression<Func<TValue>> valueExpression = () => value;

            var parameterExpression = Expression.Parameter(instance.ElementType, "x");
            var attributeExpression = Expression.PropertyOrField(
                parameterExpression,
                property);

            var equalExpression = Expression.Equal(attributeExpression, valueExpression.Body);

            var delegateType = typeof(Func<,>).MakeGenericType(instance.ElementType, typeof(bool));
            var filterExpression = Expression.Lambda(delegateType, equalExpression, parameterExpression);

            var typedMethod = OpenGenericWhereMethod.MakeGenericMethod(instance.ElementType);
            var result = typedMethod.Invoke(null, new object[] { instance, filterExpression });

            return (TQueryable)result;
        }

        public static IQueryable<TEntity> ApplyFilter<TEntity, TValue>(
            this IQueryable<TEntity> instance,
            string property,
            TValue value) where TEntity : class
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            Expression<Func<TValue>> valueExpression = () => value;

            var parameterExpression = Expression.Parameter(typeof(TEntity), "x");
            var attributeExpression = Expression.PropertyOrField(
                parameterExpression,
                property);

            var equalExpression = Expression.Equal(attributeExpression, valueExpression.Body);

            var filterExpression = Expression.Lambda<Func<TEntity, bool>>(
                equalExpression,
                parameterExpression);

            var result = instance.Where(filterExpression);

            return result;
        }


        public static IQueryable<TEntity> ExcludeDeleted<TEntity>(
            this IQueryable<TEntity> instance) where TEntity : class
        {
            return ApplyFilter(instance, "DeletedAt", (DateTime?)null);
        }
    }
}