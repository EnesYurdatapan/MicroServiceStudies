using Ms.Web.Models;
using MS.Web.Models;

namespace Ms.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync(RequestDto requestDto);
    }
}
