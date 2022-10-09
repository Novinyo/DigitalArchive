using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Documents.Type
{
    public class CheckDuplicate
    {
        public class Query : IRequest<Result<bool>>
        {
          public DuplicateVm ToCheck { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<bool>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                this._mapper = mapper;
            }

            public async Task<Result<bool>> Handle(Query request, CancellationToken cancellationToken)
            {
                var isDuplicate = false;
                if (request.ToCheck.Id == null)
                {
                    isDuplicate = request.ToCheck.Type.Trim().ToLower() == "name"
                 ? await _context.DocumentTypes
                 .AnyAsync(x => x.Name == request.ToCheck.Value.Trim(), cancellationToken)
                 : await _context.DocumentTypes
                 .AnyAsync(x => x.Code == request.ToCheck.Value.Trim(), cancellationToken);
                }
                else
                {
                    isDuplicate = request.ToCheck.Type.Trim().ToLower() == "name"
                    ? await _context.DocumentTypes
                    .AnyAsync((x => x.Name == request.ToCheck.Value.Trim() && x.Id != request.ToCheck.Id), cancellationToken)
                    : await _context.DocumentTypes
                    .AnyAsync((x => x.Code == request.ToCheck.Value.Trim() && x.Id != request.ToCheck.Id), cancellationToken);
                }

                return Result<bool>.Success(isDuplicate, (int)HttpStatusCode.OK);
            }
        }
    }
}