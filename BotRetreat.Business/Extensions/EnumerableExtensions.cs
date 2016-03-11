using System;
using System.Collections.Generic;
using System.Linq;

namespace BotRetreat.Business.Extensions
{
    public static class EnumerableExtensions
    {
        public static Double AverageOrDefault(this IEnumerable<Double> list, Double defaultValue = default(Double))
        {
            return list.Any() ? list.Average() : defaultValue;
        }
    }
}