using Microsoft.AspNetCore.Mvc;

namespace sistemaVenta.AplicacionWeb.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
