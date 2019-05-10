﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HPC.DAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static partial class XExtension
    {

        #region Count API

        /// <summary>
        /// Queryer 便捷 CountAsync 方法
        /// </summary>
        public static async Task<int> CountAsync<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return await conn.Queryer<M>().Where(compareFunc).CountAsync();
        }

        /*-------------------------------------------------------------*/

        /// <summary>
        /// Queryer 便捷 CountAsync 方法
        /// </summary>
        public static int Count<M>(this XConnection conn, Expression<Func<M, bool>> compareFunc)
            where M : class, new()
        {
            return conn.Queryer<M>().Where(compareFunc).Count();
        }
        
        #endregion

    }
}
