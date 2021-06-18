using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MDH.Linq.Extensions
{
    public static class Methods
    {
        public static class StringMethods
        {
            public static MethodInfo ContainsMethod = typeof(string).GetMethod("Contains", 0, new Type[] { typeof(string) });
        }

    }
}
