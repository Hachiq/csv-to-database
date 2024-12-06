using CsvToDatabase.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CsvToDatabase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath("C:\\Dev\\CsvToDatabase\\src")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<DevelopsTodayContext>();
            optionsBuilder.UseSqlServer(connectionString);

            Console.WriteLine(connectionString);

            using var context = new DevelopsTodayContext(optionsBuilder.Options);
        }
    }
}
