using Microsoft.AspNetCore.Mvc;

namespace sistemaVenta.AplicacionWeb.Controllers
{
    public class ReporteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
