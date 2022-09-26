using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Application.Core;

namespace Application.Staffs
{
    public class Create
    {
         public class Command : IRequest<Result<Unit>>
        {
            public string Staff { get; set; }
        }
    }
}