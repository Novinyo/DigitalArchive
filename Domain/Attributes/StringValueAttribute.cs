using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Attributes
{
    public class StringValueAttribute:Attribute
    {
        public StringValueAttribute(string value)
        {
            StringValue = value;
        }

        public string StringValue { get; }
    }
}