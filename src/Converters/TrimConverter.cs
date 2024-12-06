using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;

namespace CsvToDatabase.Converters
{
    public class TrimConverter : StringConverter
    {
        public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            return text?.Trim();
        }
    }
}
