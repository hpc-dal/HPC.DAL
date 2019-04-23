﻿using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Enums;
using HPC.DAL.Core.Extensions;
using HPC.DAL.Impls.Base;
using HPC.DAL.Interfaces.IAsyncs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HPC.DAL.Impls.ImplAsyncs
{
    internal sealed class TopAsyncImpl<M>
    : ImplerAsync
    , ITopAsync<M>
    where M : class
    {
        internal TopAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<List<M>> TopAsync(int count, IDbTransaction tran = null)
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            PreExecuteHandle(UiMethodEnum.TopAsync);
            DSA.Tran = tran;
            return await DSA.ExecuteReaderMultiRowAsync<M>();
        }
        public async Task<List<VM>> TopAsync<VM>(int count, IDbTransaction tran = null)
            where VM : class
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            SelectMQ<M, VM>();
            PreExecuteHandle(UiMethodEnum.TopAsync);
            DSA.Tran = tran;
            return await DSA.ExecuteReaderMultiRowAsync<VM>();
        }
        public async Task<List<T>> TopAsync<T>(int count, Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null)
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                DSA.Tran = tran;
                return await DSA.ExecuteReaderSingleColumnAsync(columnMapFunc.Compile());
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                DSA.Tran = tran;
                return await DSA.ExecuteReaderMultiRowAsync<T>();
            }
        }

    }

    internal sealed class TopXAsyncImpl
: ImplerAsync
, ITopXAsync
    {
        public TopXAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<List<M>> TopAsync<M>(int count, IDbTransaction tran = null)
            where M : class
        {
            SelectMHandle<M>();
            DC.PageIndex = 0;
            DC.PageSize = count;
            PreExecuteHandle(UiMethodEnum.TopAsync);
            DSA.Tran = tran;
            return await DSA.ExecuteReaderMultiRowAsync<M>();
        }
        public async Task<List<T>> TopAsync<T>(int count, Expression<Func<T>> columnMapFunc, IDbTransaction tran = null)
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                DSA.Tran = tran;
                return await DSA.ExecuteReaderSingleColumnAsync<T>();
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                DSA.Tran = tran;
                return await DSA.ExecuteReaderMultiRowAsync<T>();
            }
        }

    }
}
