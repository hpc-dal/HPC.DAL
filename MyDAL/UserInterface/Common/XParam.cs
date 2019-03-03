﻿namespace MyDAL
{
    /// <summary>
    /// SQL 参数
    /// </summary>
    public sealed class XParam
    {
        /// <summary>
        /// 参数 -- 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数 -- 值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 参数 -- 类型
        /// </summary>
        public ParamTypeEnum Type { get; set; }

        /// <summary>
        /// 参数 -- 方向(in or out)
        /// </summary>
        public ParamDirectionEnum Direction { get; set; }

        /// <summary>
        /// 参数 -- 大小(单位字节)
        /// </summary>
        public int? Size { get; set; }

        /// <summary>
        /// 参数 -- 精度(最大位数)
        /// </summary>
        public byte? Precision { get; set; }

        /// <summary>
        /// 参数 -- 小数位数
        /// </summary>
        public byte? Scale { get; set; }
    }
}