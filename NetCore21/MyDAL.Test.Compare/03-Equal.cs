﻿using HPC.DAL;
using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.Compare
{
    public class _03_Equal
        : TestBase
    {

        [Fact]
        public async Task Equal()
        {

            xx = string.Empty;

            // == --> =
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel == AgentLevel.DistiAgent)
                .QueryListAsync();

            Assert.True(res1.Count == 555);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            xx = string.Empty;
        }

        [Fact]
        public async Task NotEqual()
        {
            xx = string.Empty;

            // !(==) --> <>
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => !(it.AgentLevel == AgentLevel.DistiAgent))
                .QueryListAsync();

            Assert.True(res1.Count == 28065);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            xx = string.Empty;

            // != --> <>
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(it => it.AgentLevel != AgentLevel.DistiAgent)
                .QueryListAsync();

            Assert.True(res2.Count == 28065);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /********************************************************************************************************************************/

            xx = string.Empty;
        }

    }
}
