using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace CsvToDatabase.Converters
{
    public class EstToUtcDateTimeConverter : DateTimeConverter
    {
        private static readonly string[] DateFormats =
        {
            "MM/dd/yyyy hh:mm:ss tt",  // US format with time
            "dd/MM/yyyy hh:mm:ss tt",  // British format with time
            "yyyy-MM-dd hh:mm:ss tt",  // ISO format
            "MM/dd/yyyy",              // US format without time
            "dd/MM/yyyy"               // British format without time
        };

        public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return default(DateTime);
            }

            foreach (var format in DateFormats)
            {
                if (DateTime.TryParseExact(text, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    return TimeZoneInfo.ConvertTimeToUtc(parsedDate, est);
                }
            }

            return default(DateTime);
        }
    }
}
