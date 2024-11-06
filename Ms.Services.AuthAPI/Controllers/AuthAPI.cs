using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ms.Services.AuthAPI.Models.Dto;
using Ms.Services.AuthAPI.Service.IService;
using MS.Services.AuthAPI.Models.Dto;

namespace Ms.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPI : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _responseDto;

        public AuthAPI(IAuthService authService)
        {
            _authService = authService;
            _responseDto = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDto registrationRequestDto)
        {
            var errorMessage = await _authService.RegisterAsync(registrationRequestDto);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = errorMessage;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var loginResponse = await _authService.LoginAsync(loginRequestDto);
            if (loginResponse.User == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Username or password is incorrect";
                return BadRequest(_responseDto);
            }
            _responseDto.Result= loginResponse;
            return Ok(_responseDto);

        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegisterationRequestDto registerationRequestDto)
        {
            var assignRoleSuccesful = await _authService.AssignRole(registerationRequestDto.Email,registerationRequestDto.Role.ToUpper());
            if (!assignRoleSuccesful)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error encountered";
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);

        }
    }
}
