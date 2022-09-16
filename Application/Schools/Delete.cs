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

namespace Application.Schools
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

                var school = await _context.Schools.SingleOrDefaultAsync(x => x.Id == request.Id && x.DeletedAt == null);

                if (school == null) return null;

                school.Active = false;
                school.DeletedAt = DateTime.UtcNow;
                school.DeletedBy = userId;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete the school", (int)HttpStatusCode.BadRequest);

                return Result<Unit>.Success(Unit.Value, (int)HttpStatusCode.OK);
            }
        }
    }
}