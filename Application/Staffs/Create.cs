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

namespace Application.Staffs
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public StaffWDto Staff { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;
            private readonly UserManager<AppUser> _userManager;
            private readonly CustomSettings _customSettings;
            public Handler(DataContext context, IUserAccessor userAccessor, IMapper mapper,
            IOptions<CustomSettings> customSettings, UserManager<AppUser> userManager)
            {
                _userAccessor = userAccessor;
                _context = context;
                _mapper = mapper;
                _userManager = userManager;
                _customSettings = customSettings.Value;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var userId = _userAccessor.GetUserId();

                    var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

                    if (user == null) return Result<Unit>.Failure("Unauthorized operation",
                   (int)HttpStatusCode.Unauthorized);

                     var school = await _context.Schools.FirstOrDefaultAsync(x => x.Id == request.Staff.SchoolId, cancellationToken);

                    var newUser = _mapper.Map<AppUser>(request.Staff.User);
                    var existingUser = await _userManager.FindByNameAsync(newUser.UserName);
                    if (existingUser != null)
                        return Result<Unit>.Failure("Username already exists", (int)HttpStatusCode.BadRequest);

                    existingUser = await _userManager.FindByEmailAsync(newUser.Email);
                    if (existingUser != null)
                        return Result<Unit>.Failure("Email already exists", (int)HttpStatusCode.BadRequest);

                      var folderPath = $"{_customSettings.FilePath}\\{school.Code}";

                    var fileName = $"{newUser.UserName}_{school.Code}";
                    var path = DocumentConverter.LoadImage(request.Staff.User.Avatar, folderPath, fileName);

                    newUser.ProfilePicture = path;
                    newUser.EmailConfirmed = true;
                    newUser.CreatedAt = System.DateTime.UtcNow;
                    newUser.CreatedBy = userId;
                    

                    var result = await _userManager.CreateAsync(newUser);

                    if (result.Succeeded)
                    {
                        var roles = request.Staff.Roles;
                        foreach (var role in roles)
                        {
                            var currentRole = await _context.Roles.FindAsync(role);
                            await _userManager.AddToRoleAsync(user, currentRole.Name);
                        }

                        var staff = _mapper.Map<Staff>(request.Staff);
                       
                        var staffType = await _context.StaffTypes.FirstOrDefaultAsync(x => x.Id == request.Staff.StaffTypeId);
                        staff.User = newUser;
                        staff.School = school;
                        staff.StaffType = staffType;
                        staff.Code = $"{newUser.UserName}-{school.Code}";

                        await _context.Staffs.AddAsync(staff);

                        var isCreated = await _context.SaveChangesAsync() > 0;

                        if (!isCreated) return Result<Unit>.Failure("Failed to create Staff type",
                         (int)HttpStatusCode.BadRequest);


                        await transaction.CommitAsync();
                        return Result<Unit>.Success(Unit.Value, (int)HttpStatusCode.Created);
                    }
                    
                    
                    return Result<Unit>.Failure("Cannot create a new staff", (int)HttpStatusCode.BadRequest);
                }
                catch (System.Exception)
                {
                    await transaction.RollbackAsync();
                    return Result<Unit>.Failure("Internal server error", (int)HttpStatusCode.InternalServerError);
                }

            }
        }
    }
}