﻿using HPC.DAL.Core.Enums;
using HPC.DAL.UserFacade.Join;
using System;
using System.Linq.Expressions;

namespace HPC.DAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static class FromEx
    {

        public static FromX From<M>(this Queryer join, Expression<Func<M>> tableModelFunc)
        {
            join.DC.Action = ActionEnum.From;
            var dic = join.DC.XE.FuncTExpression(tableModelFunc);
            join.DC.DPH.AddParameter(dic);
            join.DC.SetTbMs<M>(dic.TbAlias);
            return new FromX(join.DC);
        }

    }
}
