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

namespace Application.Students
{
    public class List
    {
        public class Query : IRequest<Result<List<EntityTypeDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<EntityTypeDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<List<EntityTypeDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var staffTypes = await _context.StaffTypes.Where(x => x.Active == true && x.DeletedBy == null)
                .ProjectTo<EntityTypeDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
                
                return Result<List<EntityTypeDto>>.Success(staffTypes, (int)HttpStatusCode.OK);
            }
        }
    }
}