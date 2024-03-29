﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace Intranet.Entities;

public static class QueryableExtensions
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicateIfTrue)
        => condition ? query.Where(predicateIfTrue) : query;
}
