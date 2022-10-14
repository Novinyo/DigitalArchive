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
    public class All
    {
        public class Query : IRequest<Result<List<StudentDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<StudentDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<List<StudentDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var students = await _context.Students.Where(x => x.DeletedBy == null)
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
                
                return Result<List<StudentDto>>.Success(students, (int)HttpStatusCode.OK);
            }
        }
    }
}