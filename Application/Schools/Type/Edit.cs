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

namespace Application.Schools.Type
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public CommonDto SchoolType { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
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
                _mapper = mapper;
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = _userAccessor.GetUserId();

                if (userId == null) return Result<Unit>.Failure("Unauthorized operation",
                (int)HttpStatusCode.Unauthorized);

                var schoolType = await _context.SchoolTypes.FirstOrDefaultAsync(x => x.Id == request.SchoolType.Id
                && x.DeletedAt == null);

                if (schoolType == null) return Result<Unit>.Failure("No matching school type found",
                 (int)HttpStatusCode.NotFound);

                var existingSchooltype = await _context.SchoolTypes
                   .FirstOrDefaultAsync(x => x.Id != request.SchoolType.Id && (x.Code == request.SchoolType.Code
                   || x.Name == request.SchoolType.Name));

                if (existingSchooltype != null)
                {
                    if (existingSchooltype.Code == request.SchoolType.Code)
                        return Result<Unit>.Failure("School type code must be unique",
                         (int)HttpStatusCode.BadRequest);

                    if (existingSchooltype.Name == request.SchoolType.Name)
                        return Result<Unit>.Failure("School type name must be unique",
                         (int)HttpStatusCode.BadRequest);

                }


                _mapper.Map(request.SchoolType, schoolType);

                schoolType.ModifiedAt = DateTime.UtcNow;
                schoolType.ModifiedBy = userId;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update the school type",
                 (int)HttpStatusCode.BadRequest);

                return Result<Unit>.Success(Unit.Value, (int)HttpStatusCode.OK);
            }
        }
    }
}