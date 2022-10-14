using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Authentication;
using Application.Interfaces;
using Application.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<Result<UserDto>>> Login(LoginDto login)
        {
            return await _authService.LoginAsync(login);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<Result<UserDto>>> Register(RegisterDto user)
        {
            return await _authService.RegisterAsync(user);
        }

        [HttpPost("changepassword")]
        public async Task<ActionResult<UserResponseDto>> ChangePassword(AuthPassDto request)
        {
            return await _authService.ChangePasswordAsync(request);
        }

        [HttpGet("currentuser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var claim = User.FindFirstValue(ClaimTypes.Email);

            return await _authService.GetCurrentUser(claim);
        }

        [HttpGet("loadroles")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult<List<RoleDto>>> LoadRoles()
        {
            return await _authService.GetRoles();
        }
    }
}