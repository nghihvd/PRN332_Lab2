using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PRN232.Lab2.CoffeeStore.Repositories.Database;

namespace PRN232.Lab1.CoffeeStore.Data.Database
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            // Đường dẫn đến thư mục chứa project API, giả sử cách Infrastructure 1 cấp thư mục
            var apiProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "PRN232.Lab2.CoffeeStore.API");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(apiProjectPath) // trỏ đúng đến thư mục chứa appsettings.Development.json
                .AddJsonFile("appsettings.Development.json", optional: false)
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Connection String is not found");

            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
