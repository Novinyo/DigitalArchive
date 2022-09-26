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

namespace Application.StaffTypes
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public EntityTypeAddDto StaffType { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.StaffType.Code).NotEmpty();
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
                _context = context;
                _userAccessor = userAccessor;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var userId = _userAccessor.GetUserId();

                    var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

                    if (user == null) return Result<Unit>.Failure("Unauthorized operation", 
                    (int)HttpStatusCode.Unauthorized);

                    var staff = await _context.Staffs.FirstOrDefaultAsync(x => x.User.Id == user.Id);

                    Guid? schoolId = staff?.SchoolId != null ? staff.SchoolId : null;

                    var staffType = await _context.StaffTypes
                    .FirstOrDefaultAsync(x => (x.Code == request.StaffType.Code 
                    && x.SchoolId == schoolId)
                    || (x.Name == request.StaffType.Name && x.SchoolId == schoolId));

                    if (staffType != null)
                    {
                        if (staffType.Code == request.StaffType.Code)
                            return Result<Unit>.Failure("Staff type code must be unique",
                             (int)HttpStatusCode.BadRequest);

                        if (staffType.Name == request.StaffType.Name)
                            return Result<Unit>.Failure("Staff type name must be unique",
                             (int)HttpStatusCode.BadRequest);

                    }
                    var entity = _mapper.Map<StaffType>(request.StaffType);
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.CreatedBy = user.Id;
                    entity.Id = Guid.NewGuid();
                    entity.SchoolId = schoolId;

                    _context.StaffTypes.Add(entity);

                    var result = await _context.SaveChangesAsync() > 0;

                    if (!result) return Result<Unit>.Failure("Failed to create Staff type",
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