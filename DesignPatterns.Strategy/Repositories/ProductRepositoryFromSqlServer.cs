using BaseProject.Models;
using DesignPatterns.Strategy.Models;
using Microsoft.EntityFrameworkCore;

namespace DesignPatterns.Strategy.Repositories
{
    public class ProductRepositoryFromSqlServer(AppIdentityDbContext context) : IProductRepository
    {
        public async Task<Product> Create(Product product)
        {
            product.Id=Guid.NewGuid().ToString();
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return product;
        }

        public async Task Delete(Product product)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllByUserId(string userId) => await context.Products.Where(x => x.UserId == userId).ToListAsync();


        public async Task<Product> GetById(string id) => await context.Products.FindAsync(id);
        

        public async Task Update(Product product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();
        }
    }
}
