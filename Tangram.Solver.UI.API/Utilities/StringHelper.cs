using System;
using System.Linq;

namespace Tangram.Solver.UI.Utilities
{
    public static class StringHelper
    {
        public static string ToSpaceSeparatedString(string value)
        {
            try
            {
                var result = string.Concat(value.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');

                return result;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

    }
}