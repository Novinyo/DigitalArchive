using System;
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

namespace Application.StaffTypes
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public EntityTypeAddDto StaffType { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.StaffType.Name).NotEmpty();
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
                var staffType = await _context.StaffTypes.FirstOrDefaultAsync(x => x.Id == request.StaffType.Id
                && x.DeletedAt == null);

                if (staffType == null) return Result<Unit>.Failure("No matching staff type found",
                 (int)HttpStatusCode.NotFound);

                var existingStaffType = await _context.StaffTypes
                   .FirstOrDefaultAsync(x => x.Id != request.StaffType.Id &&
                    ((x.Code == request.StaffType.Code && x.SchoolId == schoolId
                    )
                   ||( x.Name == request.StaffType.Name && x.SchoolId == schoolId
                   )));

                if (existingStaffType != null)
                {
                    if (existingStaffType.Code == request.StaffType.Code)
                        return Result<Unit>.Failure("Staff type code must be unique",
                         (int)HttpStatusCode.BadRequest);

                    if (existingStaffType.Name == request.StaffType.Name)
                        return Result<Unit>.Failure("Staff type name must be unique",
                         (int)HttpStatusCode.BadRequest);

                }


                _mapper.Map(request.StaffType, staffType);

                staffType.ModifiedAt = DateTime.UtcNow;
                staffType.ModifiedBy = userId;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update staff type",
                 (int)HttpStatusCode.BadRequest);

                return Result<Unit>.Success(Unit.Value, (int)HttpStatusCode.OK);
            }
        }
    }
}