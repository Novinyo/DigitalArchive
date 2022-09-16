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

namespace Application.Schools.Type
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public CommonDto SchoolType { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.SchoolType.Code).NotEmpty();
                RuleFor(x => x.SchoolType.Name).NotEmpty();
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

                    var schooltype = await _context.SchoolTypes
                    .FirstOrDefaultAsync(x => x.Code == request.SchoolType.Code 
                    || x.Name == request.SchoolType.Name);

                    if (schooltype != null)
                    {
                        if (schooltype.Code == request.SchoolType.Code)
                            return Result<Unit>.Failure("School type code must be unique",
                             (int)HttpStatusCode.BadRequest);

                        if (schooltype.Name == request.SchoolType.Name)
                            return Result<Unit>.Failure("School type name must be unique",
                             (int)HttpStatusCode.BadRequest);

                    }
                    var school = _mapper.Map<SchoolType>(request.SchoolType);
                    school.CreatedAt = DateTime.UtcNow;
                    school.CreatedBy = user.Id;
                    school.Id = Guid.NewGuid();

                    _context.SchoolTypes.Add(school);

                    var result = await _context.SaveChangesAsync() > 0;

                    if (!result) return Result<Unit>.Failure("Failed to create School type",
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