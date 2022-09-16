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
    public class Details
    {
         public class Query : IRequest<Result<CommonDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<CommonDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                this._mapper = mapper;
            }

            public async Task<Result<CommonDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var schoolType = await _context.SchoolTypes.ProjectTo<CommonDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                return Result<CommonDto>.Success(schoolType, (int)HttpStatusCode.OK);
            }
        }
    }
}