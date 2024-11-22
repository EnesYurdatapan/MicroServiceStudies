using Ms.Web.Models;
using Ms.Web.Service.IService;
using Ms.Web.Utility;

namespace Ms.Web.Service
{
    public class CartService : ICartService
    {
        private readonly IBaseService _baseService;

        public CartService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = Utility.StaticDetails.ApiType.POST,
                Data = cartDto,
                Url = StaticDetails.ShoppingCartAPIBase + "/api/cart/ApplyCoupon"
            });
        }

        public async Task<ResponseDto?> EmailCart(CartDto cartDto)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = Utility.StaticDetails.ApiType.POST,
                Data = cartDto,
                Url = StaticDetails.ShoppingCartAPIBase + "/api/cart/EmailCartRequest"
            });
        }

        public async Task<ResponseDto?> GetCartByUserIdAsync(string userId)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = Utility.StaticDetails.ApiType.GET,
                Url = StaticDetails.ShoppingCartAPIBase + "/api/cart/GetCart/"+userId
            });
        }

        public async Task<ResponseDto?> RemoveFromCartAsync(int carDetailsId)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = Utility.StaticDetails.ApiType.DELETE,
                Data = carDetailsId,
                Url = StaticDetails.ShoppingCartAPIBase + "/api/cart/RemoveCart"
            });
        }

        public async Task<ResponseDto?> UpsertCartAsync(CartDto cartDto)
        {
            var response = await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = Utility.StaticDetails.ApiType.POST,
                Data = cartDto,
                Url = StaticDetails.ShoppingCartAPIBase + "/api/cart/CartUpsert"
            });
            return response;
        }
    }
}
