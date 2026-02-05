using Microsoft.AspNetCore.Mvc;

namespace ApiBase_Datos.Controllers
{
    public class SupCotroller
    {

        public class ProductoController : Controller
        {
            public IActionResult Index()
            {
                return View();
            }

            public IActionResult Crear()
            {
                return View();
            }

        }
    }
}
