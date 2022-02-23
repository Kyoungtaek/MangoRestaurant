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
    }
}
