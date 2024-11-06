using Ms.Web.Models;

namespace Ms.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync(RequestDto requestDto, bool withBearer=true);
    }
}
