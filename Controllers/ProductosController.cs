using ApiBase_Datos.Models;
using ApiBase_Datos.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiBase_Datos.Controllers
{
    public class ProductosController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var lista = await _service.ObtenerTodos();
            return View(lista); 
        }
        private readonly ProductosService _service;

        public ProductosController(ProductosService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Crear(Productos p)
        {
            if (!ModelState.IsValid)
                return View(p);

            await _service.Insertar(p);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Eliminar(long id)
        {
            await _service.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Editar(long id)
        {
            var producto = (await _service.ObtenerTodos())
                           .FirstOrDefault(p => p.Pro_Id == id);

            if (producto == null)
                return NotFound();

            return View(producto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Productos p)
        {
            if (!ModelState.IsValid)
                return View(p);

            await _service.Actualizar(p);
            return RedirectToAction(nameof(Index));
        }
    }
}
