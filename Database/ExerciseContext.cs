using Microsoft.EntityFrameworkCore;

namespace Exercise.Database
{
    public class ExerciseContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ExerciseContext(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={_configuration.GetSection("DatabaseFileName").Value}");
        }

        public DbSet<DbProduct> Products { get; set; }

        public class DbProduct
        {
            public Guid Id { get; set; }
            public string? Name { get; set; }
            public double Price { get; set; }
        }
    }
}
