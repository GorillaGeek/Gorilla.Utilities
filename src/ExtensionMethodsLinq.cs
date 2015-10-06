using Gorilla.Utilities.ExpressionVisitor;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Gorilla.Utilities
{
    public static class ExtensionMethodsLinq
    {
        /// <summary>
        /// Ordena pelo nome do campo a tabela
        /// </summary>
        /// <typeparam name="T">tabela</typeparam>
        /// <param name="datasource">query base</param>
        /// <param name="propertyName">nome do campo da tabela</param>
        /// <param name="direction">direção do sort</param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string sortColumn, string direction)
        {
            var methodName = string.Format("OrderBy{0}",
                direction == "Descending" ? "Descending" : "");
            var parameter = Expression.Parameter(query.ElementType, "p");

            MemberExpression memberAccess = null;
            foreach (var property in sortColumn.Split('.'))
            {
                memberAccess = Expression.Property(memberAccess ?? ((Expression)parameter), property);
            }

            var orderByLambda = Expression.Lambda(memberAccess, parameter);
            var result = Expression.Call(
                      typeof(Queryable),
                      methodName,
                      new[] { query.ElementType, memberAccess.Type },
                      query.Expression,
                      Expression.Quote(orderByLambda));

            return query.Provider.CreateQuery<T>(result);
        }

        private static Expression<T> ComposeLambda<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression 
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.ComposeLambda(second, Expression.And);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.ComposeLambda(second, Expression.Or);
        }
    }
}
