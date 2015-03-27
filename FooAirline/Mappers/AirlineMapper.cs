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

        public static ReadOnlyCollection<PassengerViewModel> Map(IEnumerable<DbPassenger> dbPassengers)
        {
            return dbPassengers
                .Select(Map)
                .ToList()
                .AsReadOnly();
        }

        public static PassengerViewModel Map(DbPassenger dbPassenger)
        {
            return new PassengerViewModel
            {
                FirstName = dbPassenger.FirstName,
                MiddleName = dbPassenger.MiddleName,
                LastName = dbPassenger.LastName
            };
        }
    }
}