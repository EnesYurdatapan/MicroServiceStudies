using Ms.Web.Models;
using Ms.Web.Service.IService;
using Ms.Web.Utility;

namespace Ms.Web.Service
{
    public class ProductService:IProductService
    {
        private readonly IBaseService _baseService;

        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateProductsAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = productDto,
                Url = StaticDetails.ProductAPIBase + "/api/product/"
            });
        }

        public async Task<ResponseDto?> DeleteProductsAsync(int id)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = Utility.StaticDetails.ApiType.DELETE,
                Url = StaticDetails.ProductAPIBase + "/api/product/" + id
            });
        }

        public async Task<ResponseDto?> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = Utility.StaticDetails.ApiType.GET,
                Url = StaticDetails.ProductAPIBase + "/api/product"

            });
        }

        //public async Task<ResponseDto?> GetCouponAsync(string couponCode)
        //{
        //    return await _baseService.SendAsync(new Models.RequestDto()
        //    {
        //        ApiType = Utility.StaticDetails.ApiType.GET,
        //        Url = StaticDetails.CouponAPIBase + "/api/coupon/GetByCode/" + couponCode
        //    });
        //}

        public async Task<ResponseDto?> GetProductByIdAsync(int id)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = Utility.StaticDetails.ApiType.GET,
                Url = StaticDetails.ProductAPIBase + "/api/product/" + id
            });
        }

        public async Task<ResponseDto?> UpdateProductsAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = StaticDetails.ApiType.PUT,
                Data = productDto,
                Url = StaticDetails.ProductAPIBase + "/api/product/"
            });
        }
    }
}
