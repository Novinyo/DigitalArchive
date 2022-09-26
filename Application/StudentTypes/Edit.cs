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

namespace Application.StudentTypes
{
    public class Edit
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
                _mapper = mapper;
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = _userAccessor.GetUserId();

                if (userId == null) return Result<Unit>.Failure("Unauthorized operation",
                (int)HttpStatusCode.Unauthorized);

                var staff = await _context.Staffs.FirstOrDefaultAsync(x => x.User.Id == userId);

                    Guid? schoolId = staff?.SchoolId != null ? staff.SchoolId : null;

                var studentType = await _context.StudentTypes
                .FirstOrDefaultAsync(x => x.Id == request.StudentType.Id
                && x.DeletedAt == null);

                if (studentType == null) return Result<Unit>.Failure("No matching student type found",
                 (int)HttpStatusCode.NotFound);

                var existingStudentType = await _context.StudentTypes
                   .FirstOrDefaultAsync(x => x.Id != request.StudentType.Id &&
                    ((x.Code == request.StudentType.Code && x.SchoolId == schoolId) 
                    || (x.Name == request.StudentType.Name && x.SchoolId == schoolId)));

                if (existingStudentType != null)
                {
                    if (existingStudentType.Code == request.StudentType.Code)
                        return Result<Unit>.Failure("Student type code must be unique",
                         (int)HttpStatusCode.BadRequest);

                    if (existingStudentType.Name == request.StudentType.Name)
                        return Result<Unit>.Failure("Student type name must be unique",
                         (int)HttpStatusCode.BadRequest);

                }


                _mapper.Map(request.StudentType, studentType);

                studentType.ModifiedAt = DateTime.UtcNow;
                studentType.ModifiedBy = userId;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update student type",
                 (int)HttpStatusCode.BadRequest);

                return Result<Unit>.Success(Unit.Value, (int)HttpStatusCode.OK);
            }
        }
    }
}