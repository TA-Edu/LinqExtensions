using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MDH.Linq.Extensions
{
    public static class DynamicSearchExtension
    {
        public static IQueryable<TEntity> Search<TEntity, TInput>(this IQueryable<TEntity> query, TInput input)
        {
            var inputProperties = typeof(TInput).GetProperties();
            var filters = new List<Filter>();

            foreach (var prop in inputProperties)
            {
                object[] attrs = prop.GetCustomAttributes(true);

                var filter = new Filter
                {
                    Value = prop.GetValue(input)
                };

                foreach (var attr in attrs)
                {
                    if (attr is CompareTypeAttribute compareTypeAttribute)
                    {
                        filter.CompareType = compareTypeAttribute.CompareType;
                    }

                    if (attr is FieldNameAttribute fieldNameAttribute)
                    {
                        string name = fieldNameAttribute.Name;
                        filter.Field = name;
                        filters.Add(filter);
                    }
                }
            }

            foreach (var f in filters)
            {
                if (f.Value != null)
                {

                    Expression<Func<TEntity, bool>> whereEx;
                    var constantEx = Expression.Constant(f.Value);
                    ParameterExpression pe = Expression.Parameter(typeof(TEntity), "s");


                    var propEx = Expression.Property(pe, f.Field);

                    switch (f.CompareType)
                    {
                        case CompareType.Equal:
                            {
                                whereEx = Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(propEx, constantEx), pe);
                                break;
                            }
                        case CompareType.Contains:
                            {
                                whereEx = Expression.Lambda<Func<TEntity, bool>>(Expression.Call(propEx, Methods.StringMethods.ContainsMethod, constantEx), pe);
                                break;
                            }
                        case CompareType.LessThan:
                            {
                                whereEx = Expression.Lambda<Func<TEntity, bool>>(Expression.LessThan(propEx, constantEx));
                                break;
                            }
                        case CompareType.LessThanOrEqual:
                            {
                                whereEx = Expression.Lambda<Func<TEntity, bool>>(Expression.LessThanOrEqual(propEx, constantEx));
                                break;
                            }
                        case CompareType.GreaterThan:
                            {
                                whereEx = Expression.Lambda<Func<TEntity, bool>>(Expression.GreaterThan(propEx, constantEx));
                                break;
                            }
                        case CompareType.GreaterThanOrEqual:
                            {
                                whereEx = Expression.Lambda<Func<TEntity, bool>>(Expression.GreaterThanOrEqual(propEx, constantEx));
                                break;
                            }
                        case CompareType.NotEqual:
                            {
                                whereEx = Expression.Lambda<Func<TEntity, bool>>(Expression.NotEqual(propEx, constantEx));
                                break;
                            }
                        default:
                            {
                                whereEx = Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(propEx, constantEx), pe);
                                break;
                            }
                    }

                    query = query.Where(whereEx);

                }
            }
            return query;
        }

        public static IQueryable<TEntity> Paging<TEntity, TInput>(this IQueryable<TEntity> query, int Skip, int Take)
        {
            return query;
        }

        public static IQueryable<TEntity> Sorting<TEntity, TInput>(this IQueryable<TEntity> query, TInput input)
        {
            return query;
        }
    }
}
