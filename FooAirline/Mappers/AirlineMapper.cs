using FooAirline.Models;
using FooAirline.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace FooAirline.Mappers
{
    public static class AirlineMapper
    {
        public static ReadOnlyCollection<FlightViewModel> Map(IEnumerable<DbFlight> dbFlights)
        {
            return dbFlights
                .Select(Map)
                .ToList()
                .AsReadOnly();
        }

        public static FlightViewModel Map(DbFlight dbFlight)
        {
            return new FlightViewModel
            {
                Id = dbFlight.Id,
                FlightNumber = dbFlight.FlightNumber
            };
        }
    }
}