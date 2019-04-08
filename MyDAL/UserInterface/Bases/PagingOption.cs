﻿namespace HPC.DAL
{
    /// <summary>
    ///     分页查询设置
    /// </summary>
    public abstract class PagingOption
    {
        /// <summary>
        ///     当前页
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        ///     页面大小
        /// </summary>
        public int PageSize { get; set; } = 10;
    }

}
