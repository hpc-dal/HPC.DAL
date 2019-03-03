﻿using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryVM
{
    public class _01_QueryOneAsync 
        : TestBase
    {
        [Fact]
        public async Task test()
        {

            xx = string.Empty;

            /****************************************************************************************************************************************/

            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b"))
                .QueryOneAsync<AgentVM>();

            Assert.NotNull(res1);
            Assert.Null(res1.XXXX);

            tuple = (XDebug.SQL, XDebug.Parameters,XDebug.SqlWithParams);

            /****************************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}