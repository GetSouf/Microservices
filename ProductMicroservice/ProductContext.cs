using ProductMicroservice.Models;
using Microsoft.EntityFrameworkCore;
namespace ProductMicroservice

{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options): base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
