using Xunit;
using FuncSample.Lib;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuncSample.Lib.Tests
{
    public class MyClassTests
    {
        public static IEnumerable<object[]> TestCases()
        {
            var query = "select * from items where active = 1";

            yield return new object[] {
                query,
                null,
                query
            };

            yield return new object[] {
                query,
                new Dictionary<string, bool>(),
                query
            };

            yield return new object[] {
                query,
                new Dictionary<string, bool>() {
                    { "columnA", true }
                },
                $"{query} AND (columnA = 1)"
            };

            yield return new object[] {
                query,
                new Dictionary<string, bool>() { 
                    { "columnA", true }, 
                    { "columnB", false }, 
                    { "columnC", true } 
                },
                $"{query} AND (columnA = 1 OR columnC = 1)"
            };

            yield return new object[] {
                query,
                new Dictionary<string, bool>() {
                    { "columnA", true },
                    { "columnB", false },
                    { "columnC", true },
                    { "columnD", false },
                    { "columnE", true },
                    { "columnF", true }
                },
                $"{query} AND (columnA = 1 OR columnC = 1 OR columnE = 1 OR columnF = 1)"
            };

            yield return new object[] {
                query,
                new Dictionary<string, bool>() {
                    { "columnA", true },
                    { "columnB", true }
                },
                $"{query} AND (columnA = 1 OR columnB = 1)"
            };
        }

        [Theory()]
        [MemberData(nameof(TestCases))]
        public void AppendOrBlockToQueryTest(string query, Dictionary<string, bool> orFilters, string expectedResult)
        {
            var result = SqlQueryHelper.AppendOrBlockToQuery(query, orFilters);

            Assert.Equal(expectedResult, result);
        }
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
}