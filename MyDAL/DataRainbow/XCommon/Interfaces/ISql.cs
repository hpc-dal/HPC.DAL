﻿using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using System.Collections.Generic;
using System.Text;

namespace MyDAL.DataRainbow.XCommon.Interfaces
{
    internal interface ISql
    {
        void ObjLeftSymbol(StringBuilder sb);
        void ObjRightSymbol(StringBuilder sb);

        /*************************************************************************************************************************************************************/

        void Top(Context dc, StringBuilder sb);
        void Column(string tbAlias, string colName, StringBuilder sb);
        void ColumnReplaceNullValueForSum(string tbAlias, string colName, StringBuilder sb);
        void TableX(string table, StringBuilder sb);
        void OneEqualOneProcess(DicParam p, StringBuilder sb);
        void WhereTrueOrFalse(Context dc, bool flag, StringBuilder sb);
        ColumnInfo GetIndex(List<ColumnInfo> cols);
        void Pager(Context dc, StringBuilder sb);
    }
}
