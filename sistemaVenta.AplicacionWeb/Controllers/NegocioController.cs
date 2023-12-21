using Microsoft.AspNetCore.Mvc;

namespace sistemaVenta.AplicacionWeb.Controllers
{
    public class NegocioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
