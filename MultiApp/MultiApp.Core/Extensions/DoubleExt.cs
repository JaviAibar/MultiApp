using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiApp.Core.Extensions
{
    public static class DoubleExt
    {
        public static double ParseOr0(this string value)
        {
            if (string.IsNullOrEmpty(value)) return 0;
            return double.Parse(value);
        }
    }
}
