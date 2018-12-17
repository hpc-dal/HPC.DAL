﻿using MyDAL.Test.Entities.EasyDal_Exchange;
using MyDAL.Test.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyDAL.Test.WhereEdge
{
    public class _09_WhereMultiCondition:TestBase
    {
        [Fact]
        public async Task test()
        {
            var xx1 = "";

            var guid1 = Guid.Parse("000cecd5-56dc-4085-804b-0165443bdf5d");
            var pathId1 = "~00-d-3-2-1-c-2-f-4-3-1-2-4";
            var res1 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == guid1 && it.PathId == pathId1)
                .ListAsync();
            Assert.True(res1.Count == 1);

            var tuple1 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            var xx2 = "";

            var guid2 = Guid.Parse("000a9465-8665-40bf-90e3-0165442d9120");
            var pathId2 = "~00-d-3-1-1-5-1-3-4-2-2-1-1-11-6-1-8-4";
            var res2 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == guid2 || it.PathId == pathId2)
                .ListAsync();
            Assert.True(res2.Count == 2);

            var tuple2 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            var xx3 = "";

            var guid3 = Guid.Parse("000a9465-8665-40bf-90e3-0165442d9120");
            var pathId3 = "~00-d-3-1-1-5-1-3-4-2-2-1-1-11-6-1-8-4";
            var res3 = await Conn
                .Queryer<Agent>()
                .Where(it => it.CreatedOn >= WhereTest.StartTime && it.CreatedOn <= WhereTest.EndTime)
                .ListAsync();
            Assert.True(res3.Count == 28619);

            var tuple3 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            var xx4 = "";

            var guid4 = Guid.Parse("000cecd5-56dc-4085-804b-0165443bdf5d");
            var pathId4 = "~00-d-3-2-1-c-2-f-4-3-1-2-4";
            var level4 = AgentLevel.Customer;
            var res4 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == guid4 && it.AgentLevel==level4  && it.PathId == pathId4)
                .ListAsync();
            Assert.True(res4.Count == 1);

            var tuple4 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            var xx5 = "";

            var guid5 = Guid.Parse("000cecd5-56dc-4085-804b-0165443bdf5d");
            var guid51 = Guid.Parse("000f5f16-5502-4324-b5d6-016544300263");
            var guid52 = Guid.Parse("00141f0d-7040-4b54-a6c3-016544451224");
            var res5 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == guid5 || it.Id == guid51 || it.Id == guid52)
                .ListAsync();
            Assert.True(res5.Count == 3);

            var tuple5 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            var xx6 = "";

            var guid6 = Guid.Parse("000cecd5-56dc-4085-804b-0165443bdf5d");
            var guid61 = Guid.Parse("000f5f16-5502-4324-b5d6-016544300263");
            var guid62 = Guid.Parse("00141f0d-7040-4b54-a6c3-016544451224");
            var res6 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == guid6 && it.Id == guid61 || it.Id == guid62)
                .ListAsync();
            Assert.True(res6.Count == 1);
            Assert.True(res6.First().Id == guid62);

            var tuple6 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            var xx7 = "";

            var guid7 = Guid.Parse("000cecd5-56dc-4085-804b-0165443bdf5d");
            var guid71 = Guid.Parse("000f5f16-5502-4324-b5d6-016544300263");
            var guid72 = Guid.Parse("00141f0d-7040-4b54-a6c3-016544451224");
            var res7 = await Conn
                .Queryer<Agent>()
                .Where(it => it.Id == guid7 || it.Id == guid71 && it.Id == guid72)
                .ListAsync();
            Assert.True(res7.Count == 1);
            Assert.True(res7.First().Id == guid7);

            var tuple7 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            var xx8 = "";

            var guid8 = Guid.Parse("000cecd5-56dc-4085-804b-0165443bdf5d");
            var guid81 = Guid.Parse("000f5f16-5502-4324-b5d6-016544300263");
            var guid82 = Guid.Parse("00141f0d-7040-4b54-a6c3-016544451224");
            var pathId83 = "~00-d-3-2-1-c-1-1-1-3";
            var res8 = await Conn
                .Queryer<Agent>()
                .Where(it => (it.Id == guid8 || it.Id == guid81) && it.Id == guid82||it.PathId== pathId83)
                .ListAsync();
            Assert.True(res8.Count == 1);
            Assert.True(res8.First().PathId == pathId83);

            var tuple8 = (XDebug.SQL, XDebug.Parameters, XDebug.SqlWithParams);

            /***************************************************************************************************************************/

            var xx = "";
        }
    }
}
