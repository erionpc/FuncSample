using System;
using System.Collections.Generic;
using System.Linq;

namespace FuncSample.Lib
{
    public static class Extensions
    {
        public static IEnumerable<IEnumerable<T>> GetCombinations<T>(this IEnumerable<T> source)
        {
            if (null == source)
                throw new ArgumentNullException(nameof(source));

            T[] data = source.ToArray();

            return Enumerable
                .Range(0, 1 << (data.Length))
                .Select(index => data
                    .Where((v, i) => (index & (1 << i)) != 0)
                .ToArray())
                .Where(x => x.Any());
        }
    }
}
