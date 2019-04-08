﻿using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Common;
using System.Collections.Generic;
using System.Text;

namespace HPC.DAL.DataRainbow.XCommon.Interfaces
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
