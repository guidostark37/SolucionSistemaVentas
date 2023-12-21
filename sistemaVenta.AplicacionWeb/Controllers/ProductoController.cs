using Microsoft.AspNetCore.Mvc;

namespace sistemaVenta.AplicacionWeb.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
