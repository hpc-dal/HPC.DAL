﻿using HPC.DAL.AdoNet;
using HPC.DAL.Core;
using HPC.DAL.Core.Enums;
using HPC.DAL.Impls.ImplAsyncs;
using HPC.DAL.Impls.ImplSyncs;
using HPC.DAL.ModelTools;
using HPC.DAL.UserFacade.Create;
using HPC.DAL.UserFacade.Delete;
using HPC.DAL.UserFacade.Join;
using HPC.DAL.UserFacade.Query;
using HPC.DAL.UserFacade.Update;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HPC.DAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static class XExtension
    {
        #region Internal
        /* 内部方法 */
        private static Creater<M> Creater<M>(this IDbConnection conn)
            where M : class, new()
        {
            var dc = new XContext<M>(conn)
            {
                Crud = CrudEnum.Create
            };
            return new Creater<M>(dc);
        }
        private static void CheckCreate(string sql)
        {
            if (sql.IsNullStr())
            {
                throw new Exception("Create SQL 语句不能为空！");
            }
            if (!sql.Contains("insert")
                && !sql.Contains("INSERT")
                && !sql.Contains("Insert"))
            {
                throw new Exception("Create API 只能用来新增数据！");
            }
        }
        private static void CheckDelete(string sql)
        {
            if (sql.IsNullStr())
            {
                throw new Exception("Delete SQL 语句不能为空！");
            }
            if (!sql.Contains("delete")
                && !sql.Contains("DELETE")
                && !sql.Contains("Delete"))
            {
                throw new Exception("Delete API 只能用来删除数据！");
            }
        }
        [Obsolete("警告：此 API 后面会移除！！！", false)]
        public static async Task<int> ExecuteNonQueryAsync(this IDbConnection conn, string sql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            var dc = new XContext(conn)
            {
                Crud = CrudEnum.SQL
            };
            dc.ParseSQL(sql);
            dc.ParseParam(dbParas);
            return await new ExecuteNonQuerySQLAsyncImpl(dc).ExecuteNonQueryAsync(tran);
        }
        [Obsolete("警告：此 API 后面会移除！！！", false)]
        public static int ExecuteNonQuery(this IDbConnection conn, string sql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            var dc = new XContext(conn)
            {
                Crud = CrudEnum.SQL
            };
            dc.ParseSQL(sql);
            dc.ParseParam(dbParas);
            return new ExecuteNonQuerySQLImpl(dc).ExecuteNonQuery(tran);
        }

        /*-------------------------------------------------------------*/

        [Obsolete("警告：此 API 后面会移除！！！", false)]
        public static async Task<PagingResult<T>> QueryPagingAsync<T>
            (this IDbConnection conn, PagingResult<T> paging, string totalCountSql, string pageDataSql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            var dc = new XContext(conn)
            {
                Crud = CrudEnum.SQL
            };
            dc.PageIndex = paging.PageIndex;
            dc.PageSize = paging.PageSize;
            dc.ParseSQL(totalCountSql, pageDataSql);
            dc.ParseParam(dbParas);
            var result = await new QueryPagingSQLAsyncImpl(dc).QueryPagingAsync<T>(tran);
            paging.TotalCount = result.TotalCount;
            paging.Data = result.Data;
            return paging;
        }
        [Obsolete("警告：此 API 后面会移除！！！", false)]
        public static PagingResult<T> QueryPaging<T>
            (this IDbConnection conn, PagingResult<T> paging, string totalCountSql, string pageDataSql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            var dc = new XContext(conn)
            {
                Crud = CrudEnum.SQL
            };
            dc.PageIndex = paging.PageIndex;
            dc.PageSize = paging.PageSize;
            dc.ParseSQL(totalCountSql, pageDataSql);
            dc.ParseParam(dbParas);
            var result = new QueryPagingSQLImpl(dc).QueryPaging<T>(tran);
            paging.TotalCount = result.TotalCount;
            paging.Data = result.Data;
            return paging;
        }
        #endregion

        #region Deleter
        /// <summary>
        /// 删除数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Deleter<M> Deleter<M>(this IDbConnection conn)
            where M : class, new()
        {
            var dc = new XContext<M>(conn)
            {
                Crud = CrudEnum.Delete
            };
            return new Deleter<M>(dc);
        }
        #endregion

        #region Updater
        /// <summary>
        /// 修改数据 方法簇
        /// </summary>
        /// <typeparam name="M">M:与DB Table 一 一对应</typeparam>
        public static Updater<M> Updater<M>(this IDbConnection conn)
            where M : class, new()
        {
            var dc = new XContext<M>(conn)
            {
                Crud = CrudEnum.Update
            };
            return new Updater<M>(dc);
        }
        #endregion

        #region Queryer
        /// <summary>
        /// 单表查询 方法簇
        /// </summary>
        /// <typeparam name="M1">M1:与DB Table 一 一对应</typeparam>
        public static Queryer<M1> Queryer<M1>(this IDbConnection conn)
            where M1 : class, new()
        {
            var dc = new XContext<M1>(conn)
            {
                Crud = CrudEnum.Query
            };
            return new Queryer<M1>(dc);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Queryer Queryer<M1, M2>(this IDbConnection conn, out M1 table1, out M2 table2)
            where M1 : class, new()
            where M2 : class, new()
        {
            table1 = new M1();
            table2 = new M2();
            var dc = new XContext<M1, M2>(conn)
            {
                Crud = CrudEnum.Join
            };
            return new Queryer(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Queryer Queryer<M1, M2, M3>(this IDbConnection conn, out M1 table1, out M2 table2, out M3 table3)
            where M1 : class, new()
            where M2 : class, new()
            where M3 : class, new()
        {
            table1 = new M1();
            table2 = new M2();
            table3 = new M3();
            var dc = new XContext<M1, M2, M3>(conn)
            {
                Crud = CrudEnum.Join
            };
            return new Queryer(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Queryer Queryer<M1, M2, M3, M4>(this IDbConnection conn, out M1 table1, out M2 table2, out M3 table3, out M4 table4)
            where M1 : class, new()
            where M2 : class, new()
            where M3 : class, new()
            where M4 : class, new()
        {
            table1 = new M1();
            table2 = new M2();
            table3 = new M3();
            table4 = new M4();
            var dc = new XContext<M1, M2, M3, M4>(conn)
            {
                Crud = CrudEnum.Join
            };
            return new Queryer(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Queryer Queryer<M1, M2, M3, M4, M5>(this IDbConnection conn, out M1 table1, out M2 table2, out M3 table3, out M4 table4, out M5 table5)
            where M1 : class, new()
            where M2 : class, new()
            where M3 : class, new()
            where M4 : class, new()
            where M5 : class, new()
        {
            table1 = new M1();
            table2 = new M2();
            table3 = new M3();
            table4 = new M4();
            table5 = new M5();
            var dc = new XContext<M1, M2, M3, M4, M5>(conn)
            {
                Crud = CrudEnum.Join
            };
            return new Queryer(dc);
        }
        /// <summary>
        /// 连接查询 方法簇
        /// </summary>
        public static Queryer Queryer<M1, M2, M3, M4, M5, M6>(this IDbConnection conn, out M1 table1, out M2 table2, out M3 table3, out M4 table4, out M5 table5, out M6 table6)
            where M1 : class, new()
            where M2 : class, new()
            where M3 : class, new()
            where M4 : class, new()
            where M5 : class, new()
            where M6 : class, new()
        {
            table1 = new M1();
            table2 = new M2();
            table3 = new M3();
            table4 = new M4();
            table5 = new M5();
            table6 = new M6();
            var dc = new XContext<M1, M2, M3, M4, M5, M6>(conn)
            {
                Crud = CrudEnum.Join
            };
            return new Queryer(dc);
        }
        #endregion

        #region Create API
        /// <summary>
        /// Creater 便捷 CreateAsync 方法
        /// </summary>
        public static async Task<int> CreateAsync<M>(this IDbConnection conn, M m, IDbTransaction tran = null)
            where M : class, new()
        {
            return await conn.Creater<M>().CreateAsync(m, tran);
        }

        /*-------------------------------------------------------------*/

        public static async Task<int> CreateAsync(this IDbConnection conn, string sql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            CheckCreate(sql);
            return await conn.ExecuteNonQueryAsync(sql, dbParas, tran);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Creater 便捷 CreateAsync 方法
        /// </summary>
        public static int Create<M>(this IDbConnection conn, M m, IDbTransaction tran = null)
            where M : class, new()
        {
            return conn.Creater<M>().Create(m, tran);
        }

        /*-------------------------------------------------------------*/

        public static int Create(this IDbConnection conn, string sql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            CheckCreate(sql);
            return conn.ExecuteNonQuery(sql, dbParas, tran);
        }
        #endregion

        #region CreateBatch API
        /// <summary>
        /// Creater 便捷 CreateBatchAsync 方法
        /// </summary>
        public static async Task<int> CreateBatchAsync<M>(this IDbConnection conn, IEnumerable<M> mList, IDbTransaction tran = null)
            where M : class, new()
        {
            return await conn.Creater<M>().CreateBatchAsync(mList, tran);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Creater 便捷 CreateBatchAsync 方法
        /// </summary>
        public static int CreateBatch<M>(this IDbConnection conn, IEnumerable<M> mList, IDbTransaction tran = null)
            where M : class, new()
        {
            return conn.Creater<M>().CreateBatch(mList, tran);
        }
        #endregion

        #region Delete API
        /// <summary>
        /// Deleter 便捷 DeleteAsync 方法
        /// </summary>
        public static async Task<int> DeleteAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return await conn.Deleter<M>().Where(compareFunc).DeleteAsync(tran);
        }

        /*-------------------------------------------------------------*/

        public static async Task<int> DeleteAsync(this IDbConnection conn, string sql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            CheckDelete(sql);
            return await conn.ExecuteNonQueryAsync(sql, dbParas, tran);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Deleter 便捷 DeleteAsync 方法
        /// </summary>
        public static int Delete<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return conn.Deleter<M>().Where(compareFunc).Delete(tran);
        }

        /*-------------------------------------------------------------*/

        public static int Delete(this IDbConnection conn, string sql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            CheckDelete(sql);
            return conn.ExecuteNonQuery(sql, dbParas, tran);
        }
        #endregion

        #region Update API
        /// <summary>
        /// 请参阅: <see langword=".UpdateAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<int> UpdateAsync<M>
            (this IDbConnection conn, Expression<Func<M, bool>> compareFunc, dynamic filedsObject, IDbTransaction tran = null, SetEnum set = SetEnum.AllowedNull)
            where M : class, new()
        {
            return await conn.Updater<M>().Set(filedsObject as object).Where(compareFunc).UpdateAsync(tran, set);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Updater 便捷 UpdateAsync update fields 方法
        /// </summary>
        public static int Update<M>
            (this IDbConnection conn, Expression<Func<M, bool>> compareFunc, dynamic filedsObject, IDbTransaction tran = null, SetEnum set = SetEnum.AllowedNull)
            where M : class, new()
        {
            return conn.Updater<M>().Set(filedsObject as object).Where(compareFunc).Update(tran, set);
        }
        #endregion

        #region QueryOne API
        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<M> QueryOneAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(tran);
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<VM> QueryOneAsync<M, VM>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
            where VM : class
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync<VM>(tran);
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<T> QueryOneAsync<M, T>
            (this IDbConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryOneAsync(columnMapFunc, tran);
        }

        /*-------------------------------------------------------------*/

        public static async Task<T> QueryOneAsync<T>(this IDbConnection conn, string sql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            var dc = new XContext(conn)
            {
                Crud = CrudEnum.SQL
            };
            dc.ParseSQL(sql);
            dc.ParseParam(dbParas);
            return await new QueryOneSQLAsyncImpl(dc).QueryOneAsync<T>(tran);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Queryer 便捷-同步 QueryOneAsync 方法
        /// </summary>
        public static M QueryOne<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(tran);
        }
        /// <summary>
        /// Queryer 便捷-同步 QueryOneAsync 方法
        /// </summary>
        public static VM QueryOne<M, VM>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
            where VM : class
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne<VM>(tran);
        }
        /// <summary>
        /// Queryer 便捷-同步 QueryOneAsync 方法
        /// </summary>
        public static T QueryOne<M, T>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryOne(columnMapFunc, tran);
        }

        /*-------------------------------------------------------------*/

        public static T QueryOne<T>(this IDbConnection conn, string sql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            var dc = new XContext(conn)
            {
                Crud = CrudEnum.SQL
            };
            dc.ParseSQL(sql);
            dc.ParseParam(dbParas);
            return new QueryOneSQLImpl(dc).QueryOne<T>(tran);
        }
        #endregion

        #region QueryList API
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<List<M>> QueryListAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(tran);
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<List<VM>> QueryListAsync<M, VM>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
            where VM : class
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync<VM>(tran);
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<List<T>> QueryListAsync<M, T>
            (this IDbConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).QueryListAsync(columnMapFunc, tran);
        }

        /*-------------------------------------------------------------*/

        public static async Task<List<T>> QueryListAsync<T>(this IDbConnection conn, string sql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            var dc = new XContext(conn)
            {
                Crud = CrudEnum.SQL
            };
            dc.ParseSQL(sql);
            dc.ParseParam(dbParas);
            return await new QueryListSQLAsyncImpl(dc).QueryListAsync<T>(tran);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static List<M> QueryList<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(tran);
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static List<VM> QueryList<M, VM>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
            where VM : class
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList<VM>(tran);
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static List<T> QueryList<M, T>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).QueryList(columnMapFunc, tran);
        }

        /*-------------------------------------------------------------*/

        public static List<T> QueryList<T>(this IDbConnection conn, string sql, List<XParam> dbParas = null, IDbTransaction tran = null)
        {
            var dc = new XContext(conn)
            {
                Crud = CrudEnum.SQL
            };
            dc.ParseSQL(sql);
            dc.ParseParam(dbParas);
            return new QueryListSQLImpl(dc).QueryList<T>(tran);
        }
        #endregion

        #region IsExist API
        /// <summary>
        /// 请参阅: <see langword=".IsExistAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public static async Task<bool> IsExistAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).IsExistAsync(tran);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Queryer 便捷-同步 IsExistAsync 方法
        /// </summary>
        public static bool IsExist<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).IsExist(tran);
        }
        #endregion

        #region Count API
        /// <summary>
        /// Queryer 便捷 CountAsync 方法
        /// </summary>
        public static async Task<int> CountAsync<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).CountAsync(tran);
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Queryer 便捷 CountAsync 方法
        /// </summary>
        public static int Count<M>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, IDbTransaction tran = null)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).Count(tran);
        } 
        #endregion

        /******************************************************************************************************************************/

        public static async Task<F> SumAsync<M, F>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, F>> propertyFunc, IDbTransaction tran = null)
            where M : class, new()
            where F : struct
        {
            return await conn.Queryer<M>().Where(compareFunc).SumAsync(propertyFunc, tran);
        }
        public static async Task<Nullable<F>> SumAsync<M, F>
            (this IDbConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, Nullable<F>>> propertyFunc, IDbTransaction tran = null)
            where M : class, new()
            where F : struct
        {
            return await conn.Queryer<M>().Where(compareFunc).SumAsync(propertyFunc, tran);
        }

        /*-------------------------------------------------------------*/

        public static F Sum<M, F>(this IDbConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, F>> propertyFunc, IDbTransaction tran = null)
    where M : class, new()
    where F : struct
        {
            return conn.Queryer<M>().Where(compareFunc).Sum(propertyFunc, tran);
        }
        public static Nullable<F> Sum<M, F>
            (this IDbConnection conn, Expression<Func<M, bool>> compareFunc, Expression<Func<M, Nullable<F>>> propertyFunc, IDbTransaction tran = null)
            where M : class, new()
            where F : struct
        {
            return conn.Queryer<M>().Where(compareFunc).Sum(propertyFunc, tran);
        }

        /******************************************************************************************************************************/

        /// <summary>
        /// 异步打开 DB 连接
        /// </summary>
        public static IDbConnection OpenAsync(this IDbConnection conn)
        {
            if (conn.State == ConnectionState.Closed)
            {
                new DataSourceAsync().OpenAsync(conn).GetAwaiter().GetResult();
            }
            return conn;
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Sql 调试跟踪 开启
        /// </summary>
        public static IDbConnection OpenDebug(this IDbConnection conn, DebugEnum type = DebugEnum.Output)
        {
            XConfig.IsDebug = true;
            XConfig.DebugType = type;
            return conn;
        }

    }
}
