using Ms.Services.AuthAPI.Models;

namespace Ms.Services.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser user,IEnumerable<string> roles);
    }
}
