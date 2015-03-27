﻿using Dapper;
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
                    dbo.Flight
                WHERE
                    IsActive = 1;";

            IEnumerable<DbFlight> dbFlights;

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                dbFlights = sqlConnection.Query<DbFlight>(query);

            return AirlineMapper.Map(dbFlights);
        }

        public static void CreateFlight(string flightNumber)
        {
            string insert = @"
                INSERT INTO dbo.Flight (FlightNumber)
                VALUES (@flightNumber);";

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                sqlConnection.Execute(insert, new { flightNumber });
        }
    }
}