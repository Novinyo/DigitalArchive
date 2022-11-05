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
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Staffs
{
    public class AllBySchool
    {
        public class Query : IRequest<Result<List<StaffRDto>>>
        {
            public Guid SchoolId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<StaffRDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _mapper = mapper;
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Result<List<StaffRDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = _userAccessor.GetUserId();
                var staffs = await _context.Staffs.Where(x => x.SchoolId == request.SchoolId 
                && x.User.Id != userId)
                .ProjectTo<StaffRDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

                return Result<List<StaffRDto>>.Success(staffs, (int)HttpStatusCode.OK);
            }
        }
    }
}