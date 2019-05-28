﻿using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Enums;
using HPC.DAL.Impls.Base;
using HPC.DAL.Interfaces.IAsyncs;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HPC.DAL.Impls.ImplAsyncs
{
    internal sealed class SumAsyncImpl<M>
        : ImplerAsync
        , ISumAsync<M>
    where M : class
    {
        public SumAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<F> SumAsync<F>(Expression<Func<M, F>> propertyFunc)
            where F : struct
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Sum;
            var dic = DC.XE.FuncMFExpression(propertyFunc);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.Sum);
            return await DSA.ExecuteScalarAsync<F>();
        }

        public async Task<F?> SumAsync<F>(Expression<Func<M, F?>> propertyFunc)
            where F : struct
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.SumNullable;
            var dic = DC.XE.FuncMFExpression(propertyFunc);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.Sum);
            return await DSA.ExecuteScalarAsync<F>();
        }
    }

    internal sealed class SumXAsyncImpl
        : ImplerAsync
        , ISumXAsync
    {
        public SumXAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<F> SumAsync<F>(Expression<Func<F>> propertyFunc)
            where F : struct
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Sum;
            var dic = DC.XE.FuncTExpression(propertyFunc);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.Sum);
            return await DSA.ExecuteScalarAsync<F>();
        }

        public async Task<F?> SumAsync<F>(Expression<Func<F?>> propertyFunc)
            where F : struct
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.SumNullable;
            var dic = DC.XE.FuncTExpression(propertyFunc);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.Sum);
            return await DSA.ExecuteScalarAsync<F>();
        }
    }
}
