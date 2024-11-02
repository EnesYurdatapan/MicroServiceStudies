using Ms.Web.Service.IService;
using Ms.Web.Utility;
using MS.Web.Models;

namespace Ms.Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateCouponsAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = Utility.StaticDetails.ApiType.POST,
                Data=couponDto,
                Url = StaticDetails.CouponAPIBase + "/api/coupon/"
            });
        }

        public async Task<ResponseDto?> DeleteCouponsAsync(int id)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = Utility.StaticDetails.ApiType.DELETE,
                Url = StaticDetails.CouponAPIBase + "/api/coupon/" + id
            });
        }

        public async Task<ResponseDto?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = Utility.StaticDetails.ApiType.GET,
                Url = StaticDetails.CouponAPIBase + "/api/coupon"

            });
        }

        public async Task<ResponseDto?> GetCouponAsync(string couponCode)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = Utility.StaticDetails.ApiType.GET,
                Url = StaticDetails.CouponAPIBase + "/api/coupon/GetByCode/"+couponCode
            });
        }

        public async Task<ResponseDto?> GetCouponByIdAsync(int id)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = Utility.StaticDetails.ApiType.GET,
                Url = StaticDetails.CouponAPIBase + "/api/coupon/"+id
            });
        }

        public async Task<ResponseDto?> UpdateCouponsAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = Utility.StaticDetails.ApiType.PUT,
                Data = couponDto,
                Url = StaticDetails.CouponAPIBase + "/api/coupon/"
            });
        }
    }
}
