using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvToDatabase.Data;
using CsvToDatabase.Entities;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Globalization;

namespace CsvToDatabase
{
    internal class Program
    {
        private const string CsvPath = @"C:\Dev\sample-cab-data.csv";
        private const string DuplicatesPath = @"C:\Dev\duplicates.csv";
        static void Main(string[] args)
        {
            if (Path.GetExtension(CsvPath) != ".csv")
            {
                throw new Exception("Wrong file format");
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath("C:\\Dev\\CsvToDatabase\\src")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<DevelopsTodayContext>();
            optionsBuilder.UseSqlServer(connectionString);

            var records = ReadCsv(CsvPath);

            var groupedRecords = records
                .GroupBy(r => new { r.TpepPickupDatetime, r.TpepDropoffDatetime, r.PassengerCount })
                .Where(g => g.Count() > 1)
                .SelectMany(g => g)
                .ToList();

            var duplicates = groupedRecords.ToList();
            var uniqueRecords = records.Except(duplicates).ToList();
            WriteToCsv(duplicates, DuplicatesPath);

            using var context = new DevelopsTodayContext(optionsBuilder.Options);
            context.BulkInsert(uniqueRecords);
        }

        private static List<ProcessedTrip> ReadCsv(string csvFilePath)
        {
            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Context.RegisterClassMap<ProcessedTripMap>();
                var records = csv.GetRecords<ProcessedTrip>().ToList();
                return records;
            }
        }

        private static void WriteToCsv(List<ProcessedTrip> records, string outputFilePath)
        {
            using (var writer = new StreamWriter(outputFilePath))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Context.RegisterClassMap<ProcessedTripMap>();
                csv.WriteRecords(records);
            }
        }
    }
    
}
