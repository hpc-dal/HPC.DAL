﻿using MyDAL.Core;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class QuerySingleValueImpl<M>
        : Impler, IQuerySingleValue<M>
    {
        internal QuerySingleValueImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<V> QuerySingleValueAsync<V>()
        {
            return await DC.DS.ExecuteScalarAsync<V>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.QuerySingleValueAsync)[0],
                DC.SqlProvider.GetParameters());
        }
    }
}
