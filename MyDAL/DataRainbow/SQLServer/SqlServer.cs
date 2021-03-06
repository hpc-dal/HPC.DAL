﻿using HPC.DAL.Core;
using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Common;
using HPC.DAL.Core.Enums;
using HPC.DAL.Core.Extensions;
using HPC.DAL.DataRainbow.XCommon.Bases;
using HPC.DAL.DataRainbow.XCommon.Interfaces;
using HPC.DAL.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPC.DAL.DataRainbow.SQLServer
{
    internal sealed class SqlServer
        : XSQL, ISql
    {

        private ISql DbSql { get; set; }
        internal SqlServer()
        {
            DbSql = this;
        }

        /*************************************************************************************************************************************************************/

        void ISql.ObjLeftSymbol(StringBuilder sb)
        {
            sb.Append('[');
        }
        void ISql.ObjRightSymbol(StringBuilder sb)
        {
            sb.Append(']');
        }
        void ISql.ParamSymbol(StringBuilder sb)
        {
            sb.Append(At);
        }

        /*************************************************************************************************************************************************************/

        void ISql.Top(Context dc, StringBuilder sb)
        {
            if (dc.PageIndex.HasValue
                && dc.PageSize.HasValue)
            {
                CRLF(sb); Tab(sb); sb.Append("top"); Spacing(sb); sb.Append(dc.PageSize);
            }
        }
        void ISql.Column(string tbAlias, string colName, StringBuilder sb)
        {
            if (!tbAlias.IsNullStr())
            {
                DbSql.TableXAlias(tbAlias, sb); Dot(sb);
            }
            if ("*".Equals(colName, StringComparison.OrdinalIgnoreCase))
            {
                sb.Append(colName);
            }
            else
            {
                DbSql.ObjLeftSymbol(sb); sb.Append(colName); DbSql.ObjRightSymbol(sb);
            }
        }
        void ISql.ColumnAlias(string colAlias, StringBuilder sb)
        {
            DbSql.ObjLeftSymbol(sb); sb.Append(colAlias); DbSql.ObjRightSymbol(sb);
        }
        void ISql.ColumnReplaceNullValueForSum(string tbAlias, string colName, StringBuilder sb)
        {
            sb.Append("isnull");
            LeftRoundBracket(sb);
            DbSql.Column(tbAlias, colName, sb); Comma(sb); sb.Append('0');
            RightRoundBracket(sb);
        }
        void ISql.TableX(string tbName, StringBuilder sb)
        {
            DbSql.ObjLeftSymbol(sb); sb.Append(tbName); DbSql.ObjRightSymbol(sb);
        }
        void ISql.TableXAlias(string tbAlias, StringBuilder sb)
        {
            DbSql.ObjLeftSymbol(sb); sb.Append(tbAlias); DbSql.ObjRightSymbol(sb);
        }
        void ISql.MultiAction(ActionEnum action, StringBuilder sb, Context dc)
        {
            if (action == ActionEnum.And)
            {
                Spacing(sb); sb.Append("and"); Spacing(sb);
            }
            else if (action == ActionEnum.Or)
            {
                Spacing(sb); sb.Append("or"); Spacing(sb);
            }
            else
            {
                throw XConfig.EC.Exception(XConfig.EC._037, action.ToString());
            }
        }
        void ISql.DbParam(string param, StringBuilder sb)
        {
            DbSql.ParamSymbol(sb); sb.Append(param);
        }
        void ISql.OneEqualOneProcess(DicParam p, StringBuilder sb)
        {
            Spacing(sb); sb.Append("1=1");
        }
        void ISql.WhereTrueOrFalse(Context dc, bool flag, StringBuilder sb)
        {
            if (flag)
            {
                Action(ActionEnum.Where, sb, dc); Spacing(sb); sb.Append("1=1");
            }
            else
            {
                Action(ActionEnum.Where, sb, dc); Spacing(sb); sb.Append("1<>1");
            }
        }
        ColumnInfo ISql.GetIndex(List<ColumnInfo> cols)
        {
            return
                cols.FirstOrDefault(it => "PK".Equals(it.KeyType, StringComparison.OrdinalIgnoreCase)) ??
                cols.FirstOrDefault(it => "No".Equals(it.IsNullable, StringComparison.OrdinalIgnoreCase)) ??
                cols.FirstOrDefault();
        }
        void ISql.Pager(Context dc, StringBuilder sb)
        {
            if (dc.PageIndex.HasValue
                && dc.PageSize.HasValue)
            {
                var start = default(int);
                if (dc.PageIndex > 0)
                {
                    start = ((dc.PageIndex - 1) * dc.PageSize).ToInt();
                }
                CRLF(sb);
                sb.Append("offset"); Spacing(sb); sb.Append(start); Spacing(sb);
                sb.Append("rows fetch next"); Spacing(sb); sb.Append(dc.PageSize); Spacing(sb); sb.Append("rows only");
            }
        }

    }

}
