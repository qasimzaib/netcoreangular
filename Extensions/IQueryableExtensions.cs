using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using app.Core.Models;

namespace app.Extensions {
	public static class IQueryableExtensions {
		public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObject, Dictionary<string, Expression<Func<T, object>>> columnMap) {
			if (String.IsNullOrWhiteSpace(queryObject.SortBy) || !columnMap.ContainsKey(queryObject.SortBy)) {
				return query;
			}

			if (queryObject.IsSortAscending) {
				return query.OrderBy(columnMap[queryObject.SortBy]);
			} else {
				return query.OrderByDescending(columnMap[queryObject.SortBy]);
			}
		}
	}
}