using Microsoft.AspNetCore.Mvc;
using Ms.Web.Models;
using Ms.Web.Service;
using Ms.Web.Service.IService;
using Newtonsoft.Json;

namespace Ms.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto>? list = new();
            ResponseDto? response = await _productService.GetAllProductsAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }

            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

		public async Task<IActionResult> ProductCreate()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ProductCreate(ProductDto model)
		{
			if (ModelState.IsValid)
			{
				ResponseDto? response = await _productService.CreateProductsAsync(model);

				if (response != null && response.IsSuccess)
				{
					TempData["success"] = "Product Created Successfully";
					return RedirectToAction(nameof(ProductIndex));
				}
				else
				{
					TempData["error"] = response?.Message;
				}
			}
			return View(model);
		}
		public async Task<IActionResult> ProductDelete(int productId)
		{
			ResponseDto? response = await _productService.GetProductByIdAsync(productId);

			if (response != null && response.IsSuccess)
			{
				ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));

				return View(model);
			}
			else
			{
				TempData["error"] = response?.Message;
			}
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> ProductDelete(ProductDto model)
		{
			ResponseDto? response = await _productService.DeleteProductsAsync(model.Id);

			if (response != null && response.IsSuccess)
			{
				TempData["error"] = "Coupon Deleted Successfully";
				return RedirectToAction(nameof(ProductIndex));

			}
			else
			{
				TempData["error"] = response?.Message;
			}
			return View(model);
		}
        public async Task<IActionResult> ProductEdit(int productId)
        {
            ResponseDto? response = await _productService.GetProductByIdAsync(productId);

            if (response != null && response.IsSuccess)
            {
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));

                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _productService.UpdateProductsAsync(model);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Product Updated Successfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(model);
        }
    }
}
