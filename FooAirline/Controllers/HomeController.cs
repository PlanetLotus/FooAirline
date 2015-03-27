using FooAirline.Models;
using FooAirline.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FooAirline.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ReadOnlyCollection<FlightViewModel> flights = AirlineService.GetActiveFlights();

            return View(flights);
        }

        [HttpPost]
        public ActionResult AddFlight()
        {
            string flightNumber = Request.Form["flightNumber"];

            if (string.IsNullOrWhiteSpace(flightNumber))
                throw new InvalidOperationException("FlightNumber must be given.");

            // TODO: Make sure an ACTIVE flight with this number doesn't already exist!

            int flightId = AirlineService.CreateFlight(flightNumber);

            FlightViewModel flight = AirlineService.GetFlight(flightId);

            // We just created the flight, so there won't be any passengers. This can be empty.
            ReadOnlyCollection<PassengerViewModel> passengers = new ReadOnlyCollection<PassengerViewModel>(new List<PassengerViewModel>());

            Tuple<FlightViewModel, ReadOnlyCollection<PassengerViewModel>> viewModel = new Tuple<FlightViewModel, ReadOnlyCollection<PassengerViewModel>>(flight, passengers);

            return View("Flight", viewModel);
        }

        public ActionResult Flight(int id)
        {
            FlightViewModel flight = AirlineService.GetFlight(id);

            ReadOnlyCollection<PassengerViewModel> passengers = AirlineService.GetPassengers(id);

            Tuple<FlightViewModel, ReadOnlyCollection<PassengerViewModel>> viewModel = new Tuple<FlightViewModel, ReadOnlyCollection<PassengerViewModel>>(flight, passengers);

            return View(viewModel);
        }

        [HttpPost]
        public void AddPassenger()
        {
            // TODO: Add proper error handling for user's benefit
            int flightId;
            if (!int.TryParse(Request.Form["flightId"], out flightId))
                return;

            string firstName = Request.Form["firstName"];
            string middleName = Request.Form["middleName"];
            string lastName = Request.Form["lastName"];

            if (flightId < 0)
                return;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                return;

            if (middleName.Trim() == "")
                middleName = null;

            AirlineService.AddPassenger(flightId, firstName, middleName, lastName);

            Response.Redirect("~/flight/" + flightId);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}