using Ms.Services.AuthAPI.Models.Dto;

namespace Ms.Services.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterationRequestDto registerationRequestDto);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
