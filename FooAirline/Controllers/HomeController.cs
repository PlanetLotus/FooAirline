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
            ReadOnlyCollection<FlightViewModel> flights = AirlineService.GetFlights();

            return View(flights);
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