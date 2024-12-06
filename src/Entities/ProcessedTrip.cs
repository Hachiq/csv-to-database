using System;
using System.Collections.Generic;

namespace CsvToDatabase.Entities;

public partial class ProcessedTrip
{
    public DateTime TpepPickupDatetime { get; set; }

    public DateTime TpepDropoffDatetime { get; set; }

    public byte PassengerCount { get; set; }

    public double TripDistance { get; set; }

    public string? StoreAndFwdFlag { get; set; }

    public short PulocationId { get; set; }

    public short DolocationId { get; set; }

    public decimal FareAmount { get; set; }

    public decimal TipAmount { get; set; }
}
