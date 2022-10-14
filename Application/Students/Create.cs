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

namespace Application.Students
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public EntityTypeAddDto StudentType { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.StudentType.Code).NotEmpty();
                RuleFor(x => x.StudentType.Name).NotEmpty();
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
                    var staff = await _context.Staffs.FirstOrDefaultAsync(x => x.User.Id == user.Id);

                    Guid schoolId = staff.SchoolId;

                    var studentType = await _context.StudentTypes
                    .FirstOrDefaultAsync(x => (x.Code == request.StudentType.Code 
                    && x.SchoolId == schoolId)
                    || (x.Name == request.StudentType.Name && x.SchoolId == schoolId));

                    if (studentType != null)
                    {
                        if (studentType.Code == request.StudentType.Code)
                            return Result<Unit>.Failure("Student type code must be unique",
                             (int)HttpStatusCode.BadRequest);

                        if (studentType.Name == request.StudentType.Name)
                            return Result<Unit>.Failure("Student type name must be unique",
                             (int)HttpStatusCode.BadRequest);

                    }
                    var entity = _mapper.Map<StudentType>(request.StudentType);
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.CreatedBy = user.Id;
                    entity.Id = Guid.NewGuid();
                    entity.SchoolId = schoolId;

                    _context.StudentTypes.Add(entity);

                    var result = await _context.SaveChangesAsync() > 0;

                    if (!result) return Result<Unit>.Failure("Failed to create Student type",
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