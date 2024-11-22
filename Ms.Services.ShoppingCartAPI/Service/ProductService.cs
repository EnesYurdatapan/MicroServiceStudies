using Ms.Services.ShoppingCartAPI.Models.Dto;
using Ms.Services.ShoppingCartAPI.Service.IService;
using MS.Services.ShoppingCartAPI.Models.Dto;
using Newtonsoft.Json;

namespace Ms.Services.ShoppingCartAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync($"/api/product");

            var apiContent = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if (apiResponse.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(apiResponse.Result));
            }
            return new List<ProductDto>();  
        }
    }
}
