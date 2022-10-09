using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Attributes;

namespace Application.Common
{
    public enum Categories
    {
        [StringValue("A")]
        A,

        [StringValue("B")]
        B,

        [StringValue("C")]
        C,

        [StringValue("D")]
        D
    }
}