using System;
using System.Collections.Generic;
using System.Text;

namespace MDH.Linq.Extensions
{
    public class CompareTypeAttribute : Attribute
    {
        public CompareType CompareType { get; set; }
        public CompareTypeAttribute(CompareType compareType)
        {
            CompareType = compareType;
        }
    }
}
