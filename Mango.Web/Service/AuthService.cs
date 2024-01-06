using Mango.Web.Models;

namespace Mango.Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            var result = await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utility.APIype.POST,
                Url = Utility.Utility.AuthAPIBase + "/api/Auth/AssignRole",
                Data = registrationRequestDto
            });

            return result;
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto registrationRequestDto)
        {
            var result = await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utility.APIype.POST,
                Url = Utility.Utility.AuthAPIBase + "/api/Auth/login",
                Data = registrationRequestDto
            }, withBearer: false);

            return result;
        }

        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            var result = await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utility.APIype.POST,
                Url = Utility.Utility.AuthAPIBase + "/api/Auth/register",
                Data = registrationRequestDto
            }, withBearer: false);

            return result;
        }
    }
}
