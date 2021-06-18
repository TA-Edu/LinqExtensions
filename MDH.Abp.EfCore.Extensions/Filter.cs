using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDH.Linq.Extensions
{
    public class Filter
    {
        public object Value { get; set; }
        public string Field { get; set; }
        public CompareType CompareType { get; set; }
    }
}
