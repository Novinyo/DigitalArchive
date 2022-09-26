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
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.StaffTypes
{
    public class DuplicateStaffType
    {
        public class Query : IRequest<Result<bool>>
        {
          public DuplicateVm ToCheck { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<bool>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _context = context;
                _mapper = mapper;
                _userAccessor = userAccessor;
            }

            public async Task<Result<bool>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = _userAccessor.GetUserId();

                if (userId == null) return Result<bool>.Failure("Unauthorized operation",
                (int)HttpStatusCode.Unauthorized);

                var staff = await _context.Staffs.FirstOrDefaultAsync(x => x.User.Id == userId);

                    Guid? schoolId = staff?.SchoolId != null ? staff.SchoolId : null;
                var isDuplicate = false;
                if (request.ToCheck.Id == null)
                {
                    isDuplicate = request.ToCheck.Type.Trim().ToLower() == "name"
                 ? await _context.StaffTypes
                 .AnyAsync(x => (x.Name == request.ToCheck.Value.Trim() && x.SchoolId == schoolId
                 ), 
                 cancellationToken)
                 : await _context.StaffTypes
                 .AnyAsync(x => (x.Code == request.ToCheck.Value.Trim() && x.SchoolId == schoolId
                 ), cancellationToken);
                }
                else
                {
                    isDuplicate = request.ToCheck.Type.Trim().ToLower() == "name"
                    ? await _context.StaffTypes
                    .AnyAsync((x => x.Name == request.ToCheck.Value.Trim() && x.SchoolId == schoolId 
                    && x.Id != request.ToCheck.Id), cancellationToken)
                    : await _context.StaffTypes
                    .AnyAsync((x => x.Code == request.ToCheck.Value.Trim() && x.SchoolId == schoolId 
                    && x.Id != request.ToCheck.Id), cancellationToken);
                }

                return Result<bool>.Success(isDuplicate, (int)HttpStatusCode.OK);
            }
        }
    }
}