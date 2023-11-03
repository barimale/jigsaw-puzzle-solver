using System;
using System.Linq;

namespace Christmas.Secret.Gifter.API
{
    public static class StringHelper
    {
        public static string ToSpaceSeparatedString(string value)
        {
            try
            {
                var result = string.Concat(value.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');

                return result;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
       
    }
}