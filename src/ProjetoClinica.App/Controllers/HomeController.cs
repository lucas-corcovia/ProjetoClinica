using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using ProjetoClinica.App.Extensions;
using ProjetoClinica.App.Models;
using System.Diagnostics;

namespace ProjetoClinica.App.Controllers
{
    [ServiceFilter(typeof(AutenticacaoFilterAttribute))]   
    public class HomeController : Controller
    {        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
        }
       
        public IActionResult Index()
        {
            return View();
        }

        [Route("/privacidade")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}