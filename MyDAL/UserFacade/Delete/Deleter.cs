﻿using HPC.DAL.Core.Bases;
using HPC.DAL.Impls.ImplAsyncs;
using HPC.DAL.Impls.ImplSyncs;
using HPC.DAL.Interfaces.IAsyncs;
using HPC.DAL.Interfaces.ISyncs;
using HPC.DAL.Interfaces.Segments;
using System;
using System.Data;
using System.Threading.Tasks;

namespace HPC.DAL.UserFacade.Delete
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class Deleter<M>
        : Operator
        , IWhereD<M>
        , IDeleteAsync, IDelete
        where M : class
    {
        internal Deleter(Context dc)
            : base(dc)
        { }

        public WhereD<M> WhereSegment
        {
            get
            {
                return new WhereD<M>(DC);
            }
        }

        /// <summary>
        /// 单表数据删除
        /// </summary>
        [Obsolete("警告：此 API 会删除表中所有数据！！！", false)]
        public async Task<int> DeleteAsync(IDbTransaction tran = null)
        {
            return await new DeleteAsyncImpl<M>(DC).DeleteAsync(tran);
        }

        /// <summary>
        /// 单表数据删除
        /// </summary>
        [Obsolete("警告：此 API 会删除表中所有数据！！！", false)]
        public int Delete(IDbTransaction tran = null)
        {
            return new DeleteImpl<M>(DC).Delete(tran);
        }
    }
}
