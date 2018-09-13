﻿using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.UserFacade.Create;
using EasyDAL.Exchange.UserFacade.Delete;
using EasyDAL.Exchange.UserFacade.Join;
using EasyDAL.Exchange.UserFacade.Query;
using EasyDAL.Exchange.UserFacade.Update;
using System;
using System.Data;
using EasyDAL.Exchange.UserFacade.Transaction;

namespace EasyDAL.Exchange
{
    public static class DbExtension
    {
        /// <summary>
        /// 新建数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Creater<M> Creater<M>(this IDbConnection connection)
        {
            return new Creater<M>(new DbContext(connection));
        }

        /// <summary>
        /// 删除数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Deleter<M> Deleter<M>(this IDbConnection connection)
        {
            return new Deleter<M>(new DbContext(connection));
        }

        /// <summary>
        /// 修改数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Setter<M> Updater<M>(this IDbConnection connection)
        {
            return new Setter<M>(new DbContext(connection));
        }

        /// <summary>
        /// 单表查询 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Selecter<M> Selecter<M>(this IDbConnection connection)
        {
            return new Selecter<M>(new DbContext(connection));
        }

        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static FromX Joiner<M1>(this IDbConnection connection,out M1 table1)
        {
            table1 = Activator.CreateInstance<M1>();
            return new FromX(new DbContext(connection));
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static FromX Joiner<M1,M2>(this IDbConnection connection,out M1 table1,out M2 table2)
        {
            table1 = Activator.CreateInstance<M1>();
            table2 = Activator.CreateInstance<M2>();
            return new FromX(new DbContext(connection));
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static FromX Joiner<M1,M2,M3>(this IDbConnection connection,out M1 table1,out M2 table2,out M3 table3)
        {
            table1 = Activator.CreateInstance<M1>();
            table2 = Activator.CreateInstance<M2>();
            table3 = Activator.CreateInstance<M3>();
            return new FromX(new DbContext(connection));
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static FromX Joiner<M1,M2,M3,M4>(this IDbConnection connection,out M1 table1,out M2 table2,out M3 table3,out M4 table4)
        {
            table1 = Activator.CreateInstance<M1>();
            table2 = Activator.CreateInstance<M2>();
            table3 = Activator.CreateInstance<M3>();
            table4 = Activator.CreateInstance<M4>();
            return new FromX(new DbContext(connection));
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static FromX Joiner<M1,M2,M3,M4,M5>(this IDbConnection connection,out M1 table1,out M2 table2,out M3 table3,out M4 table4,out M5 table5)
        {
            table1 = Activator.CreateInstance<M1>();
            table2 = Activator.CreateInstance<M2>();
            table3 = Activator.CreateInstance<M3>();
            table4 = Activator.CreateInstance<M4>();
            table5 = Activator.CreateInstance<M5>();
            return new FromX(new DbContext(connection));
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static FromX Joiner<M1,M2,M3,M4,M5,M6>(this IDbConnection connection,out M1 table1,out M2 table2,out M3 table3,out M4 table4,out M5 table5,out M6 table6)
        {
            table1 = Activator.CreateInstance<M1>();
            table2 = Activator.CreateInstance<M2>();
            table3 = Activator.CreateInstance<M3>();
            table4 = Activator.CreateInstance<M4>();
            table5 = Activator.CreateInstance<M5>();
            table6 = Activator.CreateInstance<M6>();
            return new FromX(new DbContext(connection));
        }

        /// <summary>
        /// Sql 调试跟踪 开启
        /// </summary>
        public static IDbConnection OpenDebug(this IDbConnection connection)
        {
            XDebug.Hint = true;
            return connection;
        }

        /// <summary>
        /// 事务单元
        /// </summary>
        public static Transactioner Transactioner(this IDbConnection connection)
        {
            return new Transactioner(new DbContext(connection));
        }

    }
}