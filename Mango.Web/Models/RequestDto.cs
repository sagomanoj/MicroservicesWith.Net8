using System.Security.AccessControl;
using static Mango.Web.Utility.Utility;

namespace Mango.Web.Models
{
    public class RequestDto
    {
        public APIype ApiType { get; set; } = APIype.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
