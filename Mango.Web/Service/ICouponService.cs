using Mango.Web.Models;

namespace Mango.Web.Service
{
    public interface ICouponService 
    {
        Task<ResponseDto> GetAllCoupons();
        Task<ResponseDto> GetCouponByCodeAsync(string coupnCode);
        Task<ResponseDto> GetCouponByIdAsync(int id);
        Task<ResponseDto> UpdateCouponAsync(CouponDto dto);
        Task<ResponseDto> CreateCouponAsync(CouponDto dto);
        Task<ResponseDto> DeleteCouponAsync(int id);

    }
}
