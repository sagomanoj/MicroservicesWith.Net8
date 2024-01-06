using Mango.Web.Models;

namespace Mango.Web.Service
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer= true);
    }
}
