﻿using MyDAL.Test.Entities.MyDAL_TestDB;
using MyDAL.Test.Enums;
using MyDAL.Test.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.QueryM
{
    public class _03_PagingListAsync_Option
        : TestBase
    {
        [Fact]
        public async Task Test()
        {

            /*****************************************************************************************************************************/

            xx = string.Empty;

            var option1 = new AgentQueryOption();
            option1.StartTime = WhereTest.CreatedOn;
            option1.EndTime = DateTime.Now;
            option1.AgentLevel = AgentLevel.DistiAgent;

            //   =    >=    <=
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(option1)
                .PagingListAsync();
            Assert.True(res1.TotalCount == 555);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*************************************************************************************************************************/

            xx = string.Empty;

            var option2 = new AgentQueryOption();
            option2.Id = Guid.Parse("000c1569-a6f7-4140-89a7-0165443b5a4b");
            option2.Name = "樊士芹";
            // where method
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(option2)
                .PagingListAsync();
            Assert.True(res2.TotalCount == 1);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            xx = string.Empty;

            var option3 = new AgentQueryOption();
            option3.StartTime = WhereTest.CreatedOn;
            option3.EndTime = DateTime.Now;

            //   >=   <=  
            var res3 = await Conn
                .Queryer<Agent>()
                .Where(option3)
                .PagingListAsync();
            Assert.True(res3.TotalCount == 28619);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            xx = string.Empty;

            var option4 = new AgentQueryOption();
            option4.Name = "张";

            //   like  
            var res4 = await Conn
                .Queryer<Agent>()
                .Where(option4)
                .PagingListAsync();
            Assert.True(res4.TotalCount == 2002);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            var xx4 = string.Empty;

            var option5 = new AgentQueryOption();
            option5.EnumListIn = new List<AgentLevel>
            {
                AgentLevel.CityAgent,
                AgentLevel.DistiAgent
            };

            // in
            var res5 = await Conn
                .Queryer<Agent>()
                .Where(option5)
                .PagingListAsync();
            Assert.True(res5.TotalCount == 555);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            var xx5 = string.Empty;

            var option6 = new AgentQueryOption();
            option6.EnumListNotIn = new List<AgentLevel>
            {
                AgentLevel.CityAgent,
                AgentLevel.DistiAgent
            };

            // in
            var res6 = await Conn
                .Queryer<Agent>()
                .Where(option6)
                .PagingListAsync();
            Assert.True(res6.TotalCount == 28064 || res6.TotalCount == 28065);

            tuple = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /*****************************************************************************************************************************/

            xx = string.Empty;

        }
    }
}