using Exercise.Database;
using Exercise.Repositories;
using Exercise.Services;
using Microsoft.EntityFrameworkCore;

namespace Exercise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddDependencyInjection();

            builder.Services.AddDbContext<ExerciseContext>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ExerciseContext>();
                db.Database.Migrate();
            }

            app.MapControllers();

            app.Run();
        }
    }

    public static class Extensions
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();
        }
    }
}
