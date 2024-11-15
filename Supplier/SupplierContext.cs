using SupplierMicroservice.Models;
using Microsoft.EntityFrameworkCore;
namespace SupplierMicroservice

{
    public class SupplierContext : DbContext
    {
        public SupplierContext(DbContextOptions<SupplierContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
    }
}
