﻿using MyDAL.Core.Bases;
using MyDAL.Impls;
using MyDAL.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Join
{
    public sealed class DistinctXO
        : Operator, IQueryPagingXO
    {
        internal DistinctXO(Context dc)
            : base(dc)
        { }


        /// <summary>
        /// 多表分页查询
        /// </summary>
        public async Task<PagingResult<M>> QueryPagingAsync<M>()
            where M : class
        {
            return await new PagingListXOImpl(DC).QueryPagingAsync<M>();
        }
        /// <summary>
        /// 多表分页查询
        /// </summary>
        public async Task<PagingResult<T>> QueryPagingAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            return await new PagingListXOImpl(DC).QueryPagingAsync(columnMapFunc);
        }

    }
}