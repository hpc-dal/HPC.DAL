﻿using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Extensions;
using MyDAL.DataRainbow.XCommon.Bases;
using MyDAL.DataRainbow.XCommon.Interfaces;
using MyDAL.ModelTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDAL.DataRainbow.SQLServer
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
                sb.Append(tbAlias); XSQL.Dot(sb);
            }
            DbSql.ObjLeftSymbol(sb); sb.Append(colName); DbSql.ObjRightSymbol(sb);
        }
        void ISql.TableX(string table, StringBuilder sb)
        {
            DbSql.ObjLeftSymbol(sb); sb.Append(table); DbSql.ObjRightSymbol(sb);
        }
        void ISql.OneEqualOneProcess(DicParam p, StringBuilder sb)
        {
            Spacing(sb); sb.Append("1=1");
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
                sb.Append("rows fetch next");Spacing(sb); sb.Append(dc.PageSize);Spacing(sb);sb.Append("rows only");                
            }
        }

    }

}