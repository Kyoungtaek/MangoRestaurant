using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService service;
        public ProductController(IProductService service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            var list = new List<ProductDto>();
            var response = await service.GetAllProductAsync<ResponseDto>();

            if(response!=null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await service.CreateProductAsync<ResponseDto>(model);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int productId)
        {
            var response = await service.GetProductbyIdAsync<ResponseDto>(productId);

            if (response != null && response.IsSuccess)
            {
                ProductDto product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));

                return View(product);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await service.UpdateProductAsync<ResponseDto>(model);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }
    }
}
