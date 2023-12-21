using Microsoft.AspNetCore.Mvc;

namespace sistemaVenta.AplicacionWeb.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
