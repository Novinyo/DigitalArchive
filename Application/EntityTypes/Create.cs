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

namespace Application.EntityTypes
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public CommonDto EntityTypes { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.EntityTypes.Code).NotEmpty();
                RuleFor(x => x.EntityTypes.Name).NotEmpty();
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

                    var entityType = await _context.EntityTypes
                    .FirstOrDefaultAsync(x => x.Code == request.EntityTypes.Code 
                    || x.Name == request.EntityTypes.Name);

                    if (entityType != null)
                    {
                        if (entityType.Code == request.EntityTypes.Code)
                            return Result<Unit>.Failure("Entity type code must be unique",
                             (int)HttpStatusCode.BadRequest);

                        if (entityType.Name == request.EntityTypes.Name)
                            return Result<Unit>.Failure("Entity type name must be unique",
                             (int)HttpStatusCode.BadRequest);

                    }
                    var entity = _mapper.Map<EntityType>(request.EntityTypes);
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.CreatedBy = user.Id;
                    entity.Id = Guid.NewGuid();

                    _context.EntityTypes.Add(entity);

                    var result = await _context.SaveChangesAsync() > 0;

                    if (!result) return Result<Unit>.Failure("Failed to create Entity type",
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