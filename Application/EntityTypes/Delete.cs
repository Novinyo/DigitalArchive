using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.EntityTypes
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = _userAccessor.GetUserId();

                if (userId == null) return Result<Unit>.Failure("Unauthorized operation",
                (int)HttpStatusCode.Unauthorized);

                var entityType = await _context.EntityTypes.SingleOrDefaultAsync(x => x.Id == request.Id && x.DeletedAt == null);

                if (entityType == null) return null;

                entityType.Active = false;
                entityType.DeletedAt = DateTime.UtcNow;
                entityType.DeletedBy = userId;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete entity type", (int)HttpStatusCode.BadRequest);

                return Result<Unit>.Success(Unit.Value, (int)HttpStatusCode.OK);
            }
        }
    }
}