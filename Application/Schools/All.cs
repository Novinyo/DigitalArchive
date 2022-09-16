using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Schools
{
    public class All
    {
        public class Query : IRequest<Result<List<SchoolDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<SchoolDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<List<SchoolDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var schools = await _context.Schools
                .ProjectTo<SchoolDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
                
                return Result<List<SchoolDto>>.Success(schools, (int)HttpStatusCode.OK);
            }
        }
    }
}