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

namespace Application.Documents.Type
{
    public class Details
    {
         public class Query : IRequest<Result<DocumentTypeDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<DocumentTypeDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                this._mapper = mapper;
            }

            public async Task<Result<DocumentTypeDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var documentType = await _context.DocumentTypes.ProjectTo<DocumentTypeDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                return Result<DocumentTypeDto>.Success(documentType, (int)HttpStatusCode.OK);
            }
        }
    }
}