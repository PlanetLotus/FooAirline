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
        public static ReadOnlyCollection<FlightViewModel> GetFlights()
        {
            const string query = @"
                SELECT
                    Id,
                    FlightNumber
                FROM
                    dbo.Flight;";

            IEnumerable<DbFlight> dbFlights;

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                dbFlights = sqlConnection.Query<DbFlight>(query);

            return AirlineMapper.Map(dbFlights);
        }

        public static void CreateFlight()
        {
            const string insert = @"
                INSERT INTO dbo.Flight
                VALUES (1, 100);";

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                sqlConnection.Execute(insert);
        }
    }
}