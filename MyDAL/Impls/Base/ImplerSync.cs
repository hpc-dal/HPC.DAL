﻿using HPC.DAL.AdoNet;
using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Enums;
using System;
using System.Data;

namespace HPC.DAL.Impls.Base
{
    internal abstract class ImplerSync
                : Impler
    {
        protected ImplerSync(Context dc)
            : base(dc)
        {
            DSS = new DataSourceSync(dc);
        }

        internal DataSourceSync DSS { get; private set; }

        protected PagingResult<T> PagingListAsyncHandleSync<T>(UiMethodEnum sqlType, bool single, IDbTransaction tran = null)
        {
            PreExecuteHandle(sqlType);
            DSS.Tran = tran;
            return DSS.ExecuteReaderPaging<None, T>(single, null);
        }
        protected PagingResult<T> PagingListAsyncHandleSync<M, T>(UiMethodEnum sqlType, bool single, Func<M, T> mapFunc, IDbTransaction tran = null)
           where M : class
        {
            PreExecuteHandle(sqlType);
            DSS.Tran = tran;
            return DSS.ExecuteReaderPaging(single, mapFunc);
        }
    }
}
