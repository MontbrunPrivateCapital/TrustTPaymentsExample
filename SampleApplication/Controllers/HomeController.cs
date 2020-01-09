using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleApplication.Data;
using SampleApplication.Models;
using SampleApplication.Services;

namespace SampleApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        private readonly Payment _trustt;

        public HomeController(
            DataContext context,
            Payment payment,
            ILogger<HomeController> logger)
        {
            _logger = logger;
            _trustt = payment;
            _context = context;
        }


        public IActionResult Index() => View(_trustt.MyStatus());

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
