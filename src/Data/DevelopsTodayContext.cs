using System;
using System.Collections.Generic;
using CsvToDatabase.Entities;
using Microsoft.EntityFrameworkCore;

namespace CsvToDatabase.Data;

public partial class DevelopsTodayContext : DbContext
{
    public DevelopsTodayContext()
    {
    }

    public DevelopsTodayContext(DbContextOptions<DevelopsTodayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ProcessedTrip> ProcessedTrips { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProcessedTrip>(entity =>
        {
            entity.HasNoKey();

            entity.HasIndex(e => e.PulocationId, "IX_ProcessedTrips_PULocationId");

            entity.HasIndex(e => new { e.PulocationId, e.TipAmount }, "IX_ProcessedTrips_TipAmount").IsDescending(false, true);

            entity.HasIndex(e => new { e.TpepPickupDatetime, e.TpepDropoffDatetime }, "IX_ProcessedTrips_TravelTime");

            entity.HasIndex(e => e.TripDistance, "IX_ProcessedTrips_TripDistance").IsDescending();

            entity.Property(e => e.DolocationId).HasColumnName("DOLocationID");
            entity.Property(e => e.FareAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("fare_amount");
            entity.Property(e => e.PassengerCount).HasColumnName("passenger_count");
            entity.Property(e => e.PulocationId).HasColumnName("PULocationID");
            entity.Property(e => e.StoreAndFwdFlag)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("store_and_fwd_flag");
            entity.Property(e => e.TipAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("tip_amount");
            entity.Property(e => e.TpepDropoffDatetime)
                .HasColumnType("datetime")
                .HasColumnName("tpep_dropoff_datetime");
            entity.Property(e => e.TpepPickupDatetime)
                .HasColumnType("datetime")
                .HasColumnName("tpep_pickup_datetime");
            entity.Property(e => e.TripDistance).HasColumnName("trip_distance");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
