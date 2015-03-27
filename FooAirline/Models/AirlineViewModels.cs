using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FooAirline.Models
{
    public class FlightViewModel
    {
        public string FlightNumber { get; set; }
    }

    public class PassengerViewModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}