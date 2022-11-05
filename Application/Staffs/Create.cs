using System.Threading.Tasks;
using MediatR;
using Application.Core;
using Application.Staffs.Dtos;
using System.Threading;
using Persistence;
using Application.Interfaces;
using AutoMapper;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;

namespace Application.Staffs
{
    public class Create
    {
        public class Command : IRequest<Result<StaffRDto>>
        {
            public StaffWDto Staff { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<StaffRDto>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;
            private readonly UserManager<AppUser> _userManager;
            private readonly IDocumentConverter _converter;
            public Handler(DataContext context, IUserAccessor userAccessor, IMapper mapper,
             UserManager<AppUser> userManager, IDocumentConverter converter)
            {
                _userAccessor = userAccessor;
                _context = context;
                _mapper = mapper;
                _userManager = userManager;
                _converter = converter;
            }

            public async Task<Result<StaffRDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var userId = _userAccessor.GetUserId();

                    var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

                    if (user == null) return Result<StaffRDto>.Failure("Unauthorized operation",
                   (int)HttpStatusCode.Unauthorized);

                    var school = await _context.Schools.FirstOrDefaultAsync(x => x.Id == request.Staff.SchoolId, cancellationToken);

                    var newUser = _mapper.Map<AppUser>(request.Staff.User);
                    var existingUser = await _userManager.FindByNameAsync(newUser.UserName);
                    if (existingUser != null)
                        return Result<StaffRDto>.Failure("Username already exists", (int)HttpStatusCode.BadRequest);

                    existingUser = await _userManager.FindByEmailAsync(newUser.Email);
                    if (existingUser != null)
                        return Result<StaffRDto>.Failure("Email already exists", (int)HttpStatusCode.BadRequest);

                    var path = "";

                    if (request.Staff.User.Avatar != null && request.Staff.User.Avatar.IndexOf("base64") != -1)
                    {
                        var fileName = $"{newUser.UserName}_{school.Code}";
                        path = _converter.SaveImage(request.Staff.User.Avatar, school.Code, fileName);
                    }
                    newUser.ProfilePicture = path != "" ? path : "";
                    newUser.EmailConfirmed = false;
                    newUser.CreatedAt = System.DateTime.UtcNow;
                    newUser.CreatedBy = userId;



                    var result = await _userManager.CreateAsync(newUser);

                    if (result.Succeeded)
                    {
                        var roles = request.Staff.Roles;

                        await _userManager.AddToRolesAsync(newUser, roles);

                        var staff = _mapper.Map<Staff>(request.Staff);

                        var staffType = await _context.StaffTypes.FirstOrDefaultAsync(x => x.Id == request.Staff.StaffTypeId);
                        staff.User = newUser;
                        staff.School = school;
                        staff.StaffType = staffType;
                        staff.Code = $"{newUser.UserName}-{school.Code}";

                        await _context.Staffs.AddAsync(staff);

                        var isCreated = await _context.SaveChangesAsync() > 0;

                        if (!isCreated) return Result<StaffRDto>.Failure("Failed to create Staff",
                         (int)HttpStatusCode.BadRequest);


                        await transaction.CommitAsync();

                        var createdStaff = _mapper.Map<StaffRDto>(staff);

                        return Result<StaffRDto>.Success(createdStaff, (int)HttpStatusCode.Created);
                    }


                    return Result<StaffRDto>.Failure("Cannot create a new staff", (int)HttpStatusCode.BadRequest);
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    await transaction.RollbackAsync();
                    return Result<StaffRDto>.Failure("Internal server error", (int)HttpStatusCode.InternalServerError);
                }

            }
        }
    }
}