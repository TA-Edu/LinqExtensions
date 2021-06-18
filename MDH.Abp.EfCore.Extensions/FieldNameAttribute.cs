using System;
using System.Collections.Generic;
using System.Text;

namespace MDH.Linq.Extensions
{
    public class FieldNameAttribute : Attribute
    {
        public string Name;
        public FieldNameAttribute(string name)
        {
            Name = name;
        }
    }
}
