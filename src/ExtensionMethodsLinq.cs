using Gorilla.Utilities.Enums;
using Gorilla.Utilities.ExpressionVisitor;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Gorilla.Utilities
{
    public static class ExtensionMethodsLinq
    {
        /// <summary>
        /// Order by string prop name
        /// </summary>
        /// <typeparam name="T">table</typeparam>
        /// <param name="query">query base</param>
        /// <param name="sortColumn">name of prop to sort</param>
        /// <param name="direction">asc or desc</param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string sortColumn, SortOrder direction)
        {
            var methodName = $"OrderBy{(direction == SortOrder.Descending ? "Descending" : "")}";
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

        /// <summary>
        /// Compose a new lambda using 'AND' conditional
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second">lambda to add</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.ComposeLambda(second, Expression.And);
        }

        /// <summary>
        /// Compose a new lambda using 'OR' conditional
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second">lambda to add</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.ComposeLambda(second, Expression.Or);
        }
    }
}
