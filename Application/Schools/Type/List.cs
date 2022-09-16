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

namespace Application.Schools.Type
{
    public class List
    {
        public class Query : IRequest<Result<List<CommonDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<CommonDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<List<CommonDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var schoolTypes = await _context.SchoolTypes.Where(x => x.Active == true)
                .ProjectTo<CommonDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
                
                return Result<List<CommonDto>>.Success(schoolTypes, (int)HttpStatusCode.OK);
            }
        }
    }
}