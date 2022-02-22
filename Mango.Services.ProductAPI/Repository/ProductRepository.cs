using AutoMapper;
using Mango.Services.ProductAPI.DbContexts;
using Mango.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext db;
        private IMapper mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            IEnumerable<Product> productList = await db.Products.ToListAsync();

            return mapper.Map<List<ProductDto>>(productList);
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            Product product = await db.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

            return mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            Product product = mapper.Map<ProductDto, Product>(productDto);

            if (product.ProductId > 0)
            {
                db.Products.Update(product);
            }
            else
            {
                db.Products.Add(product);
            }

            await db.SaveChangesAsync();

            return mapper.Map<Product, ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                Product product = await db.Products.FirstOrDefaultAsync(x => x.ProductId == productId);

                if(product != null)
                {
                    db.Products.Remove(product);
                    await db.SaveChangesAsync();

                    return true;
                }

                return false;
                
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
