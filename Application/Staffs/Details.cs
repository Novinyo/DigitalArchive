using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Authentication;
using Application.Common;
using Application.Core;
using Application.Staffs.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Staffs
{
    public class Details
    {
         public class Query : IRequest<Result<StaffRDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<StaffRDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly UserManager<AppUser> _userManager;

            public Handler(DataContext context, IMapper mapper, UserManager<AppUser> userManager)
            {
                _context = context;
                this._mapper = mapper;
                _userManager = userManager;
            }

            public async Task<Result<StaffRDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var staff = await _context.Staffs.ProjectTo<StaffRDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                var user = await _userManager.FindByIdAsync(staff.UserId);
                var userRoles = await _userManager.GetRolesAsync(user);
                
                var roles = new List<string>();

                foreach (var item in userRoles)
                {
                    roles.Add(item);
                }
                staff.Roles = roles;

                return Result<StaffRDto>.Success(staff, (int)HttpStatusCode.OK);
            }
        }
    }
}