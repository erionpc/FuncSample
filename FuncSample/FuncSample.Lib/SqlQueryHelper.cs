using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncSample.Lib
{
    public class SqlQueryHelper
    {
        public static string AppendOrBlockToQuery(string query, Dictionary<string, bool> orFilters)
        {
            if (!orFilters?.Any() ?? true)
                return query;

            if (string.IsNullOrWhiteSpace(query))
                return string.Empty;

            var criteria = orFilters.Select(x => Tuple.Create<Func<bool>, string>(() => x.Value, $"{x.Key} = 1"));

            return $"{query} AND ({GetOrBlock(criteria)})";
        }

        private static string GetOrBlock(IEnumerable<Tuple<Func<bool>, string>> criteria)
        {
            string orBlock = "";

            var filterCombinations = criteria.GetCombinations().OrderByDescending(x => x.Count());

            foreach (var filterCriteria in filterCombinations)
            {
                if (filterCriteria.Select(x => x.Item1).All(x => x()))
                {
                    orBlock += string.Join(" OR ", filterCriteria.Select(x => x.Item2));
                    break;
                }
            }

            return orBlock;
        }
    }
}
