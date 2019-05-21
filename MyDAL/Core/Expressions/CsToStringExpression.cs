﻿using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Common;
using HPC.DAL.Core.Enums;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HPC.DAL.Core.Expressions
{
    internal sealed class CsToStringExpression
    {
        private Context DC { get; }

        private CsToStringExpression() { }
        internal CsToStringExpression(Context dc)
        {
            DC = dc;
        }

        internal DicParam WhereFuncToString(Expression left, BinExprInfo bin)
        {
            var cp = DC.XE.GetKey(left, FuncEnum.DateFormat, CompareXEnum.None);
            var val = DC.VH.ValueProcess(bin.Right, cp.ValType, cp.Format);
            DC.Option = OptionEnum.Function;
            DC.Func = FuncEnum.DateFormat;
            DC.Compare = bin.Compare;
            var format = DC.TSH.DateTime(cp.Format);
            return DC.DPH.DateFormatDic(cp, val, format);
        }
        internal DicParam SelectFuncToString(MethodCallExpression mcExpr)
        {
            var type = mcExpr.Object.Type;
            if (type == XConfig.CSTC.String
                 || type.IsEnum)
            {
                return DC.XE.MemberAccessHandle(mcExpr.Object as MemberExpression);
            }
            else if (type == XConfig.CSTC.ByteArray)
            {
                throw XConfig.EC.Exception(XConfig.EC._093, $"【byte[]】对应 DB column 不能使用 C# .ToString() 函数！表达式--【{mcExpr.ToString()}】");
            }
            else
            {
                DC.Option = OptionEnum.ColumnAs;
                DC.Compare = CompareXEnum.None;
                var cp = DC.XE.GetKey(mcExpr, FuncEnum.DateFormat, CompareXEnum.None);
                DC.Func = FuncEnum.DateFormat;
                var format = DC.TSH.DateTime(cp.Format);
                if (DC.Action == ActionEnum.Select)
                {
                    return DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.DateFormatDic(cp, null, format) });
                }
                else
                {
                    return DC.DPH.DateFormatDic(cp, null, format);
                }
            }
        }
    }
}
