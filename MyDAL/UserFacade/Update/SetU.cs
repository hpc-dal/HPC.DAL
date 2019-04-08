﻿using System;
using System.Threading.Tasks;
using HPC.DAL.Core.Bases;
using HPC.DAL.Impls;
using HPC.DAL.Interfaces;
using HPC.DAL.Interfaces.Segments;

namespace HPC.DAL.UserFacade.Update
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class SetU<M>
        : Operator
        , IWhereU<M>
        , IUpdateAsync<M>, IUpdate<M>
        where M : class
    {

        internal SetU(Context dc)
            : base(dc)
        { }

        public WhereU<M> WhereSegment
        {
            get
            {
                return new WhereU<M>(DC);
            }
        }

        /// <summary>
        /// 单表数据更新
        /// </summary>
        /// <returns>更新条目数</returns>
        [Obsolete("警告：此 API 会更新表中所有数据！！！", false)]
        public async Task<int> UpdateAsync(SetEnum set = SetEnum.AllowedNull)
        {
            return await new UpdateAsyncImpl<M>(DC).UpdateAsync(set);
        }

        /// <summary>
        /// 单表数据更新
        /// </summary>
        /// <returns>更新条目数</returns>
        [Obsolete("警告：此 API 会更新表中所有数据！！！", false)]
        public int Update(SetEnum set = SetEnum.AllowedNull)
        {
            return new UpdateImpl<M>(DC).Update(set);
        }
    }
}
