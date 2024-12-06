using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace CsvToDatabase.Converters
{
    public class StoreAndFwdFlagConverter : StringConverter
    {
        public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            return text == "Y" ? "Yes" : text == "N" ? "No" : "No";
        }
    }
}
