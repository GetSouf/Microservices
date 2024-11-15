using ProductMicroservice;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace ProductMicroservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
           // System.Globalization.CultureInfo.DefaultThreadCurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
           // System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = System.Globalization.CultureInfo.InvariantCulture;

            builder.Services.AddDbContext<ProductContext>(options =>
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProductDB;Integrated Security=True"));
  



            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
