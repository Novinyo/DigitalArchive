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

namespace Application.EntityTypes
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public CommonDto EntityType { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.EntityType.Name).NotEmpty();
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

                var entityType = await _context.EntityTypes.FirstOrDefaultAsync(x => x.Id == request.EntityType.Id
                && x.DeletedAt == null);

                if (entityType == null) return Result<Unit>.Failure("No matching entity type found",
                 (int)HttpStatusCode.NotFound);

                var existingEntityType = await _context.EntityTypes
                   .FirstOrDefaultAsync(x => x.Id != request.EntityType.Id && (x.Code == request.EntityType.Code
                   || x.Name == request.EntityType.Name));

                if (existingEntityType != null)
                {
                    if (existingEntityType.Code == request.EntityType.Code)
                        return Result<Unit>.Failure("Entity type code must be unique",
                         (int)HttpStatusCode.BadRequest);

                    if (existingEntityType.Name == request.EntityType.Name)
                        return Result<Unit>.Failure("Entity type name must be unique",
                         (int)HttpStatusCode.BadRequest);

                }


                _mapper.Map(request.EntityType, entityType);

                entityType.ModifiedAt = DateTime.UtcNow;
                entityType.ModifiedBy = userId;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update entity type",
                 (int)HttpStatusCode.BadRequest);

                return Result<Unit>.Success(Unit.Value, (int)HttpStatusCode.OK);
            }
        }
    }
}