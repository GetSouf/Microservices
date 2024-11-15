using CategoryMicroservice;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace CategoryMicroservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            
            builder.Services.AddDbContext<CategoryContext>(options =>
               options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CategoryDB;Integrated Security=True"));

            // Configure the HTTP request pipeline.
            var app = builder.Build();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
