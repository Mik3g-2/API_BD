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
        [HttpGet]
        public async Task<IActionResult> Editar(long id)
        {
            var producto = await _service.ObtenerPorId(id);

            if (producto == null)
                return NotFound();

            return View(producto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(long id,Productos p)
        {
            if (id != p.Pro_Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(p);

            var resultado = await _service.Actualizar(p);

            if(resultado)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(p);
        }
    }
}
