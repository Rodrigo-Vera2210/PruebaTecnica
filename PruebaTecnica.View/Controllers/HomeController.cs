using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.View.Models;
using PruebaTecnica.View.Services;
using System.Diagnostics;

namespace PruebaTecnica.View.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceAPI _serviceAPI;

        public HomeController(IServiceAPI serviceAPI)
        {
            _serviceAPI = serviceAPI;
        }

        public async Task<IActionResult> Index(string modo, DateTime fechaInicio, DateTime fechaFinal)
        {
            Response respuesta = null;

            if (!string.IsNullOrEmpty(modo) || !fechaInicio.Equals(new DateTime(0001, 1, 1)) || !fechaFinal.Equals(new DateTime(0001, 1, 1)))
            {
                respuesta = await _serviceAPI.ObtenerParametrosSensor(modo, fechaInicio, fechaFinal);
            }

            return View(respuesta);
        }

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
