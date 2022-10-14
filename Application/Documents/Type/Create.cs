using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Documents.Type
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public DocumentTypeDto DocumentType { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.DocumentType.Code).NotEmpty();
                RuleFor(x => x.DocumentType.Name).NotEmpty();
                RuleFor(x => x.DocumentType.Category).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IUserAccessor userAccessor, IMapper mapper)
            {
                _context = context;
                _userAccessor = userAccessor;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var userId = _userAccessor.GetUserId();

                    var user = await _context.Users.FirstOrDefaultAsync(
                        x => x.Id == userId
                    );

                    if (user == null) return Result<Unit>.Failure("Unauthorized operation", 
                    (int)HttpStatusCode.Unauthorized);

                    var documentType = await _context.DocumentTypes
                    .FirstOrDefaultAsync(x => x.Code == request.DocumentType.Code 
                    || x.Name == request.DocumentType.Name);

                    if (documentType != null)
                    {
                        if (documentType.Code == request.DocumentType.Code)
                            return Result<Unit>.Failure("Document type code must be unique",
                             (int)HttpStatusCode.BadRequest);

                        if (documentType.Name == request.DocumentType.Name)
                            return Result<Unit>.Failure("Document type name must be unique",
                             (int)HttpStatusCode.BadRequest);

                    }
                    var document = _mapper.Map<DocumentType>(request.DocumentType);
                    document.CreatedAt = DateTime.UtcNow;
                    document.CreatedBy = user.Id;
                    document.Id = Guid.NewGuid();

                    _context.DocumentTypes.Add(document);

                    var result = await _context.SaveChangesAsync() > 0;

                    if (!result) return Result<Unit>.Failure("Failed to create document type",
                     (int)HttpStatusCode.BadRequest);

                    return Result<Unit>.Success(Unit.Value, (int)HttpStatusCode.Created);
                }
                catch (System.Exception ex)
                {
                    return Result<Unit>.Failure(ex.Message,
                     (int)HttpStatusCode.InternalServerError);
                }

            }
        }
    }
}