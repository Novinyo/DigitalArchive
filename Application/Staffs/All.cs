using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Staffs
{
    public class All
    {
        public class Query : IRequest<Result<List<StaffRDto>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<StaffRDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<List<StaffRDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var staffs = await _context.Staffs
                .ProjectTo<StaffRDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

                return Result<List<StaffRDto>>.Success(staffs, (int)HttpStatusCode.OK);
            }
        }
    }
}