using CategoryMicroservice.Models;
using Microsoft.EntityFrameworkCore;
namespace CategoryMicroservice

{
    public class CategoryContext : DbContext
    {
        public CategoryContext(DbContextOptions<CategoryContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
    }
}
