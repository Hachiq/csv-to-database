using CsvHelper.Configuration;
using CsvToDatabase.Converters;
using CsvToDatabase.Entities;

namespace CsvToDatabase
{
    public class ProcessedTripMap : ClassMap<ProcessedTrip>
    {
        public ProcessedTripMap()
        {
            Map(m => m.TpepPickupDatetime).Name("tpep_pickup_datetime")
                .TypeConverter<EstToUtcDateTimeConverter>();
            Map(m => m.TpepDropoffDatetime).Name("tpep_dropoff_datetime")
                .TypeConverter<EstToUtcDateTimeConverter>();
            Map(m => m.PassengerCount).Name("passenger_count")
                .TypeConverter<ByteOrNullConverter>();
            Map(m => m.TripDistance).Name("trip_distance");
            Map(m => m.StoreAndFwdFlag).Name("store_and_fwd_flag")
                .TypeConverter<TrimConverter>()
                .TypeConverter<StoreAndFwdFlagConverter>();
            Map(m => m.PulocationId).Name("PULocationID");
            Map(m => m.DolocationId).Name("DOLocationID");
            Map(m => m.FareAmount).Name("fare_amount");
            Map(m => m.TipAmount).Name("tip_amount");
        }
    }
}
