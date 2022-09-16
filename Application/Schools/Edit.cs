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
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public AddSchoolDto School { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.School.Name).NotEmpty();
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

                var schoolType = await _context.SchoolTypes.FirstOrDefaultAsync(x => x.Id == request.School.SchoolTypeId
               && x.DeletedAt == null);

                if (schoolType == null) return Result<Unit>.Failure("No matching school type found",
                 (int)HttpStatusCode.NotFound);

                var school = await _context.Schools.FirstOrDefaultAsync(x => x.Id == request.School.Id
                && x.DeletedAt == null);


                if (school == null) return Result<Unit>.Failure("No matching school found",
                 (int)HttpStatusCode.NotFound);

                var existingSchool = await _context.Schools
                   .FirstOrDefaultAsync(x => x.Id != request.School.Id
                   && (x.Code == request.School.Code
                   || x.Name == request.School.Name));


                if (existingSchool != null)
                {
                    if (existingSchool.Code == request.School.Code)
                        return Result<Unit>.Failure("School code must be unique",
                         (int)HttpStatusCode.BadRequest);

                    if (existingSchool.Name == request.School.Name)
                        return Result<Unit>.Failure("School name must be unique",
                         (int)HttpStatusCode.BadRequest);

                }

                _mapper.Map(request.School, school);

                school.ModifiedAt = DateTime.UtcNow;
                school.ModifiedBy = userId;
                school.SchoolType = schoolType;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update the school",
                 (int)HttpStatusCode.BadRequest);

                return Result<Unit>.Success(Unit.Value, (int)HttpStatusCode.OK);
            }
        }
    }
}