﻿using HPC.DAL.Core;
using HPC.DAL.Core.Bases;
using HPC.DAL.DataRainbow.XCommon.Interfaces;
using System;
using System.Data;

namespace HPC.DAL.DataRainbow.SQLServer
{
    internal sealed class SqlServerTypeConfig
        : IDbTypeConfig
    {

        DbType IDbTypeConfig.IntProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.Int32;
        }
        DbType IDbTypeConfig.LongProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.Int64;
        }
        DbType IDbTypeConfig.DecimalProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.Decimal;
        }
        DbType IDbTypeConfig.BoolProc(Context dc, ParamTypeEnum colType)
        {
            if (colType == ParamTypeEnum.Bit_MySQL_SqlServer)
            {
                return DbType.Int16;
            }
            else
            {
                return DbType.Boolean;
            }
        }
        DbType IDbTypeConfig.StringProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.String;
        }
        DbType IDbTypeConfig.ListStringProc(Context dc, ParamTypeEnum colType)
        {
            throw XConfig.EC.Exception(XConfig.EC._092, "sql server 不处理 List-String 此种类型的列！");
        }
        DbType IDbTypeConfig.DateTimeProc(Context dc, ParamTypeEnum colType)
        {
            if (colType == ParamTypeEnum.DateTime2_SqlServer)
            {
                return DbType.DateTime2;
            }
            else if (colType == ParamTypeEnum.DateTime_MySQL_SqlServer)
            {
                return DbType.DateTime;
            }
            else
            {
                return DbType.DateTime;
            }
        }
        DbType IDbTypeConfig.GuidProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.Guid;
        }
        DbType IDbTypeConfig.ByteProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.Byte;
        }
        DbType IDbTypeConfig.ByteArrayProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.Binary;
        }
        DbType IDbTypeConfig.CharProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.StringFixedLength;
        }
        DbType IDbTypeConfig.DoubleProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.Double;
        }
        DbType IDbTypeConfig.FloatProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.Single;
        }
        DbType IDbTypeConfig.SbyteProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.SByte;
        }
        DbType IDbTypeConfig.ShortProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.Int16;
        }
        DbType IDbTypeConfig.UintProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.UInt32;
        }
        DbType IDbTypeConfig.UlongProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.UInt64;
        }
        DbType IDbTypeConfig.UshortProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.UInt16;
        }
        DbType IDbTypeConfig.TimeSpanProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.Time;
        }
        DbType IDbTypeConfig.DateTimeOffsetProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.DateTimeOffset;
        }
        DbType IDbTypeConfig.ObjectProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.Object;
        }

    }
}
