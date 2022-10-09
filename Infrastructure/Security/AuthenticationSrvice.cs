using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Authentication;
using Application.Interfaces;
using Application.Core;
using Domain;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Domain.Enums;
using AutoMapper;
using Application.Common;

namespace Infrastructure.Security
{
    public class AuthenticationSrvice : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserAccessor _userAccessor;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AuthenticationSrvice(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,

        TokenService tokenService, RoleManager<IdentityRole> roleManager, IUserAccessor userAccessor,
         DataContext context, IMapper mapper)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _userAccessor = userAccessor;
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> ChangePasswordAsync(AuthPassDto request)
        {
            var userId = _userAccessor.GetUserId();

            if (!Guid.TryParse(userId, out Guid newId))
            {
                throw new Exception("Invalid User");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (request.NewPassword.Length < 8)
            {
                throw new Exception("Password must be at least 8 characters");
            }
            else if (!AuthenticationHelper.TimeConstantCompare(request.NewPassword, request.MatchPassword))
            {
                throw new Exception("Passwords do not match");
            }

            var hashPassword = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (hashPassword.Succeeded)
                return new UserResponseDto { UserId = user.Id };

            var errors = hashPassword.Errors.Select(x => x.Description).ToList();
            var stringError = string.Join(",", errors);

            throw new Exception($"Unable to change password: {stringError}");
        }

        public async Task<UserDto> GetCurrentUser(string emailClaim)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == emailClaim);

            return await CreateUserObject(user);
        }

        public async Task<List<RoleDto>> GetRoles()
        {
            var userId = _userAccessor.GetUserId();

             var roles = new List<IdentityRole>();

            if (!Guid.TryParse(userId, out Guid newId))
            {
                throw new Exception("Invalid User");
            }
            var dtoRoles = new List<RoleDto>();
            var user = await _userManager.FindByIdAsync(userId);

            var userRoles = await _userManager.GetRolesAsync(user);

            if(userRoles.Any(x => x == Roles.SuperAdmin.ToString()))
                roles = await _roleManager.Roles.Where(x => x.Name != Roles.SuperAdmin.ToString()).ToListAsync();
            else if(userRoles.Any(x => x == Roles.Admin.ToString()))
            {
                roles = await _roleManager.Roles.Where(x =>  (x.Name != Roles.SuperAdmin.ToString()
                 && x.Name != Roles.Admin.ToString())).ToListAsync();
            }

            foreach(var role in roles)
            {
                var current = (Roles)Enum.Parse(typeof(Roles), role.Name);
                var newRole = new RoleDto{Id = role.Name, Name = current.GetAttributeStringValue()};

                dtoRoles.Add(newRole);
            }

            return dtoRoles;
        }

        public async Task<Result<UserDto>> LoginAsync(LoginDto login)
        {
            var username = login.Username;
            AppUser user;

            if (AuthenticationHelper.IsValidEmail(username))
            {
                user = await _userManager.FindByEmailAsync(username);

                if (user == null) return Result<UserDto>.Failure($"Invalid Username or password", 400);

                username = user.UserName;
            }
            else
            {
                user = await _userManager.FindByNameAsync(username);
                if (user == null) return Result<UserDto>.Failure($"Invalid Username or password", 400);
            }

            var result = await _signInManager.PasswordSignInAsync(username, login.Password, false, false);

            if (!result.Succeeded) return Result<UserDto>.Failure($"Invalid Username or password", 400);

            var loginUser = await CreateUserObject(user);

            return Result<UserDto>.Success(loginUser, 200);
        }

        public async Task<UserDto> RegisterAsync(RegisterDto register)
        {
            if (await _userManager.Users.AnyAsync(x => x.UserName == register.Username))
                throw new Exception($"Username '{register.Username}' already taken");

            if (await _userManager.Users.AnyAsync(x => x.Email == register.Email))
                throw new Exception($"Username '{register.Username}' already taken");


            var userId = _userAccessor.GetUserId();

            if (!Guid.TryParse(userId, out Guid newId))
            {
                throw new Exception("Invalid user");
            }

            var user = new AppUser
            {
                Email = register.Email,
                FirstName = register.FirstName,
                LastName = register.LastName,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId
            };

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Staff");

                string jwtSecurityToken = await _tokenService.GenerateToken(user);
                return await CreateUserObject(user);
            }
            else
                throw new Exception($"{string.Join(", ", result.Errors.ToList())}");
        }

        private async Task<UserDto> CreateUserObject(AppUser user)
        {
            var school = new Application.Schools.UserSchoolDto();
            string token = await _tokenService.GenerateToken(user);

            var userRoles = await _userManager.GetRolesAsync(user);

            var roles = userRoles.Select(x => x.ToString().ToLower()).ToArray();
            var staff = await _context.Staffs.FirstOrDefaultAsync(x => x.User.Id == user.Id);
            if (staff != null) {
                school.SchoolId = staff.SchoolId.ToString() ?? "";
                school.Name = staff.School?.Name;
            } else {
                
            }

            var userDto = new UserDetails
            {
                Email = user.Email,
                PhotoURL =$"assets/images/avatars/{user.ProfilePicture}",
                Username = user.UserName,
                DisplayName = $"{user.FirstName} {user.LastName}",
                Settings = new Settings { Layout = new object { }, Theme = new object { } },
                Shortcuts = new string[] { "apps.calendar", "apps.mailbox", "apps.contacts" },
                School = school,
            };
            var userAuth = new UserAuthDto
            {
                Uuid = user.Id,
                From = user.UserName,
                Roles = roles,
                Data = userDto
            };

            return new UserDto
            {
                User = userAuth,
                Access_Token = token
            };
        }
    }
}