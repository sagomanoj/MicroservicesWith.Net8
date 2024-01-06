using Mango.Web.Models;

namespace Mango.Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto> CreateCouponAsync(CouponDto dto)
        {
            var result = await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utility.APIype.POST,
                Url = Utility.Utility.CouponAPIBase + "/api/coupon/",
                Data = dto
            });

            return result;
        }

        public async Task<ResponseDto> DeleteCouponAsync(int id)
        {
            var result = await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utility.APIype.DELETE,
                Url = Utility.Utility.CouponAPIBase + "/api/coupon/"+ id
            });

            return result;
        }

        public async Task<ResponseDto> GetAllCoupons()
        {
           var result = await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utility.APIype.GET,
                Url = Utility.Utility.CouponAPIBase + "/api/coupon"
            });

            return result;

        }

        public async Task<ResponseDto> GetCouponByCodeAsync(string coupnCode)
        {
            var result = await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utility.APIype.GET,
                Url = Utility.Utility.CouponAPIBase + "/api/coupon/GetByCode/" + coupnCode
            });

            return result;
        }

        public async Task<ResponseDto> GetCouponByIdAsync(int id)
        {
            var result = await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utility.APIype.GET,
                Url = Utility.Utility.CouponAPIBase + "/api/coupon/" + id
            });

            return result;
        }

        public async Task<ResponseDto> UpdateCouponAsync(CouponDto dto)
        {
            var result = await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utility.APIype.PUT,
                Url = Utility.Utility.CouponAPIBase + "/api/coupon/" ,
                Data = dto
            }); 

            return result;
        }
    }
}
