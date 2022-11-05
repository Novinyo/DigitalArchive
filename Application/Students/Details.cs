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
         public class Query : IRequest<Result<StudentDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<StudentDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                this._mapper = mapper;
            }

            public async Task<Result<StudentDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var student = await _context.Students.Include(x => x.Contact)
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                return Result<StudentDto>.Success(student, (int)HttpStatusCode.OK);
            }
        }
    }
}