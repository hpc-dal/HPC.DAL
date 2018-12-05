﻿using System;
using System.Threading;

namespace MyDAL.Core.Common
{
    internal class AutoRetry
    {
        
        internal T Invoke<P, T>(P param, Func<P, T> func)
        {
            for (var i = 0; i < XConfig.CacheRetry; i++)
            {
                try
                {
                    return func(param);
                }
                catch (Exception ex)
                {
                    Thread.Sleep(50);
                    if (i < XConfig.CacheRetry)
                    {
                        continue;
                    }
                    throw new Exception($"{func.ToString()}失败!重试次数:{XConfig.CacheRetry}次,失败原因:{ex.Message}");
                }
            }
            return default(T);
        }

    }
}
