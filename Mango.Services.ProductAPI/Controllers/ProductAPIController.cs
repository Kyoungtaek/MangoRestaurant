using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        protected ResponseDto response;
        private IProductRepository repo;

        public ProductAPIController(IProductRepository repo)
        {
            this.repo = repo;
            this.response = new ResponseDto();
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<ProductDto> productDtos = await repo.GetProducts();
                response.Result = productDtos;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }

            return response;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                ProductDto productDto = await repo.GetProductById(id);
                response.Result = productDto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }

            return response;
        }

        [HttpPost]
        public async Task<object> Post([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto model = await repo.CreateUpdateProduct(productDto);
                response.Result = model;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }

            return response;
        }

        [HttpPut]
        public async Task<object> Put([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto model = await repo.CreateUpdateProduct(productDto);
                response.Result = model;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }

            return response;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSuccess = await repo.DeleteProduct(id);
                response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }

            return response;
        }
    }
}
