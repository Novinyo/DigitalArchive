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
    public class Details
    {
         public class Query : IRequest<Result<SchoolDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<SchoolDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                this._mapper = mapper;
            }

            public async Task<Result<SchoolDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var school = await _context.Schools.ProjectTo<SchoolDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                return Result<SchoolDto>.Success(school, (int)HttpStatusCode.OK);
            }
        }
    }
}