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
    public class Details
    {
         public class Query : IRequest<Result<EntityTypeDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<EntityTypeDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                this._mapper = mapper;
            }

            public async Task<Result<EntityTypeDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var studentType = await _context.StudentTypes
                .ProjectTo<EntityTypeDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                return Result<EntityTypeDto>.Success(studentType, (int)HttpStatusCode.OK);
            }
        }
    }
}