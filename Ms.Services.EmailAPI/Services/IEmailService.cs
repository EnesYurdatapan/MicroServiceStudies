using Ms.Services.EmailAPI.Models.Dtos;

namespace Ms.Services.EmailAPI.Services
{
    public interface IEmailService
    {
        Task EmailCartAndLog(CartDto cartDto);
    }
}
