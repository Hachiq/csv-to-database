# CSV to Database Console Application

This project implements a simple ETL (Extract, Transform, Load) process that reads data from a CSV file, processes it, and inserts it into a SQL Server database.

## SQL

Below are the SQL scripts used for creating the table and indexes:

### Create Table
```sql
CREATE TABLE ProcessedTrips (
    tpep_pickup_datetime DATETIME NOT NULL,
    tpep_dropoff_datetime DATETIME NOT NULL,
    passenger_count TINYINT NOT NULL CHECK (passenger_count >= 0),
    trip_distance FLOAT NOT NULL CHECK (trip_distance >= 0),
    store_and_fwd_flag VARCHAR(3) NOT NULL CHECK (store_and_fwd_flag IN ('Yes', 'No')),
    PULocationID SMALLINT NOT NULL,
    DOLocationID SMALLINT NOT NULL,
    fare_amount DECIMAL(10, 2) NOT NULL CHECK (fare_amount >= 0),
    tip_amount DECIMAL(10, 2) NOT NULL CHECK (tip_amount >= 0)
);
```

### Create Indexes
```sql
CREATE INDEX IX_ProcessedTrips_TipAmount 
ON ProcessedTrips (PULocationID, tip_amount DESC);

CREATE INDEX IX_ProcessedTrips_TripDistance 
ON ProcessedTrips (trip_distance DESC);

CREATE INDEX IX_ProcessedTrips_TravelTime 
ON ProcessedTrips (tpep_pickup_datetime, tpep_dropoff_datetime);

CREATE INDEX IX_ProcessedTrips_PULocationId 
ON ProcessedTrips (PULocationID);
```

## Number of rows in the table after running the program.
### 29779

## Comments
I made store_and_fwd_flag column not nullable, so i transform empty values into "No". Probably, I shouldn't have done it.

I didn't really do anythig about "potentially unsafe source", except the IsValidExtension (.csv) check.

If an input file was much bigger, i would definitely use SqlBulkCopy, as it is considered to be the fastest way to insert large amound of data into DB. Also, i added some Type Converters for CsvHelper that may be optimized.