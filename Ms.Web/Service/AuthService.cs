using Ms.Web.Models;
using Ms.Web.Service.IService;
using Ms.Web.Utility;

namespace Ms.Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AssignRoleAsync(RegisterationRequestDto registerationRequestDto)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = registerationRequestDto,
                Url = StaticDetails.AuthAPIBase + "/api/auth/AssignRole"
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = loginRequestDto,
                Url = StaticDetails.AuthAPIBase + "/api/auth/login"
            }, withBearer: false);
        }

        public async Task<ResponseDto?> RegisterAsync(RegisterationRequestDto registerationRequestDto)
        {
            var x = new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = registerationRequestDto,
                Url = StaticDetails.AuthAPIBase + "/api/auth/register"
            };
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = registerationRequestDto,
                Url = StaticDetails.AuthAPIBase + "/api/auth/register"
            }, withBearer: false);
        }
    }
}
