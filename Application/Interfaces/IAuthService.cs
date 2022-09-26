using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Authentication;
using Application.Core;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result<UserDto>> LoginAsync(LoginDto login);

        Task<UserDto> RegisterAsync(RegisterDto register);

        Task<UserResponseDto> ChangePasswordAsync(AuthPassDto request);

        Task<UserDto> GetCurrentUser(string emailClaim);

        Task<List<RoleDto>> GetRoles();
    }
}