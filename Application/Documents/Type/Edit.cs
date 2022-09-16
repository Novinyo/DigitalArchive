using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Documents.Type
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public CommonDto DocumentType { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.DocumentType.Name).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IUserAccessor userAccessor, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = _userAccessor.GetUserId();

                if (userId == null) return Result<Unit>.Failure("Unauthorized operation",
                (int)HttpStatusCode.Unauthorized);

                var documentType = await _context.DocumentTypes.FirstOrDefaultAsync(x => x.Id == request.DocumentType.Id
                && x.DeletedAt == null);

                if (documentType == null) return Result<Unit>.Failure("No matching document type found",
                 (int)HttpStatusCode.NotFound);

                var existingDocumentType = await _context.DocumentTypes
                   .FirstOrDefaultAsync(x => x.Id != request.DocumentType.Id && (x.Code == request.DocumentType.Code
                   || x.Name == request.DocumentType.Name));

                if (existingDocumentType != null)
                {
                    if (existingDocumentType.Code == request.DocumentType.Code)
                        return Result<Unit>.Failure("Document type code must be unique",
                         (int)HttpStatusCode.BadRequest);

                    if (existingDocumentType.Name == request.DocumentType.Name)
                        return Result<Unit>.Failure("Document type name must be unique",
                         (int)HttpStatusCode.BadRequest);

                }


                _mapper.Map(request.DocumentType, documentType);

                documentType.ModifiedAt = DateTime.UtcNow;
                documentType.ModifiedBy = userId;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update the document type",
                 (int)HttpStatusCode.BadRequest);

                return Result<Unit>.Success(Unit.Value, (int)HttpStatusCode.OK);
            }
        }
    }
}