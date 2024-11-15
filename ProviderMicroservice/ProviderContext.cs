using ProviderMicroservice.Models;
using Microsoft.EntityFrameworkCore;
namespace ProviderMicroservice

{
    public class ProviderContext : DbContext
    {
        public ProviderContext(DbContextOptions<ProviderContext> options) : base(options)
        {
        }
        public DbSet<Provider> providers { get; set; }
    }
}
