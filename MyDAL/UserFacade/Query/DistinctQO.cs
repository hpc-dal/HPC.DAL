﻿using HPC.DAL.Core.Bases;
using HPC.DAL.Impls;
using HPC.DAL.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HPC.DAL.UserFacade.Query
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class DistinctQO<M>
        : Operator
        , IQueryPagingOAsync<M>, IQueryPagingO<M>
        where M : class
    {
        internal DistinctQO(Context dc)
            : base(dc)
        { }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        public async Task<PagingResult<M>> QueryPagingAsync()
        {
            return await new QueryPagingOAsyncImpl<M>(DC).QueryPagingAsync();
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        public async Task<PagingResult<VM>> QueryPagingAsync<VM>()
            where VM : class
        {
            return await new QueryPagingOAsyncImpl<M>(DC).QueryPagingAsync<VM>();
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        public async Task<PagingResult<T>> QueryPagingAsync<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return await new QueryPagingOAsyncImpl<M>(DC).QueryPagingAsync(columnMapFunc);
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        public PagingResult<M> QueryPaging()
        {
            return new QueryPagingOImpl<M>(DC).QueryPaging();
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        public PagingResult<VM> QueryPaging<VM>()
            where VM : class
        {
            return new QueryPagingOImpl<M>(DC).QueryPaging<VM>();
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        public PagingResult<T> QueryPaging<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return new QueryPagingOImpl<M>(DC).QueryPaging(columnMapFunc);
        }
    }
}
