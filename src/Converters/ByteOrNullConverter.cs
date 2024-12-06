using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;

namespace CsvToDatabase.Converters
{
    public class ByteOrNullConverter : ITypeConverter
    {
        public object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return (byte)0;
            }
            return byte.Parse(text);
        }

        public string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            return value?.ToString();
        }
    }
}
