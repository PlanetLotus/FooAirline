using Dapper;
using FooAirline.Mappers;
using FooAirline.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FooAirline.Services
{
    public static class AirlineService
    {
        public static ReadOnlyCollection<FlightViewModel> GetActiveFlights()
        {
            const string query = @"
                SELECT
                    Id,
                    FlightNumber
                FROM
                    dbo.Flight
                WHERE
                    IsActive = 1;";

            IEnumerable<DbFlight> dbFlights;

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                dbFlights = sqlConnection.Query<DbFlight>(query);

            return AirlineMapper.Map(dbFlights);
        }

        public static FlightViewModel GetFlight(int id)
        {
            const string query = @"
                SELECT
                    Id,
                    FlightNumber
                FROM
                    dbo.Flight
                WHERE
                    Id = @id;";

            DbFlight dbFlight;

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                dbFlight = sqlConnection.Query<DbFlight>(query, new { id }).FirstOrDefault();

            // TODO: Gracefully handle null dbFlight
            if (dbFlight == null)
                return null;

            return AirlineMapper.Map(dbFlight);
        }

        public static void CreateFlight(string flightNumber)
        {
            const string insert = @"
                INSERT INTO dbo.Flight (FlightNumber)
                VALUES (@flightNumber);";

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                sqlConnection.Execute(insert, new { flightNumber });
        }

        public static ReadOnlyCollection<PassengerViewModel> GetPassengers(int flightId)
        {
            const string query = @"
                SELECT
                    FirstName,
                    MiddleName,
                    LastName
                FROM
                    dbo.Passenger
                WHERE
                    FlightId = @flightId;";

            IEnumerable<DbPassenger> dbPassengers;

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                dbPassengers = sqlConnection.Query<DbPassenger>(query, new { flightId });

            return AirlineMapper.Map(dbPassengers);
        }

        public static void AddPassenger(int flightId, string firstName, string middleName, string lastName)
        {
            const string insert = @"
                INSERT INTO dbo.Passenger (FlightId, FirstName, MiddleName, LastName)
                VALUES (@flightId, @firstName, @middleName, @lastName);";

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                sqlConnection.Execute(insert, new { flightId, firstName, middleName, lastName });
        }
    }
}