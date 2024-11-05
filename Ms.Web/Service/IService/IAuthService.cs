using Ms.Web.Models;

namespace Ms.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto); 
        Task<ResponseDto?> RegisterAsync(RegisterationRequestDto registerationRequestDto); 
        Task<ResponseDto?> AssignRoleAsync(RegisterationRequestDto registerationRequestDto); 
    }
}
