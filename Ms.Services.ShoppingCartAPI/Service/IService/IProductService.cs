using Ms.Services.ShoppingCartAPI.Models.Dto;

namespace Ms.Services.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
    }
}
