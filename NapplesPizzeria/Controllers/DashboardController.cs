using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NapplesPizzeria.Models;
using NapplesPizzeria.Services;
using NapplesPizzeria.ViewModels;

namespace NapplesPizzeria.Controllers
{
    public class DashboardController : Controller
    {
        private readonly NaplesPizzeriaContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly MainServices _mainServices;

        public DashboardController(ILogger<HomeController> logger, NaplesPizzeriaContext context, MainServices services)
        {
            _logger = logger;
            _context = context;
            _mainServices = services;
        }

        [HttpGet]
        public IActionResult Index()
        {
          
            string username = HttpContext.Session.GetString("username");
            // SI el usuario no ha iniciado sesión, redirigir a la página de inicio
            if (username == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Dictionary<int, bool> tableState = new();
            tableState = _mainServices.estadoTablas();

            var viewModel = new DashboardViewModel()
            {
                TableState = tableState,
                Products = _context.MtabProducts.ToList(),
                Orders = _context.MtabOrders.ToList(),
                Services = _context.MtabServices.ToList(),
                Category = _context.MtabCategories.ToList()
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetProductos()
        {
            string username = HttpContext.Session.GetString("username");
            // SI el usuario no ha iniciado sesión, redirigir a la página de inicio
            if (username == null)
            {
                return RedirectToAction("Index", "Home");

            }

            var products = _context.MtabProducts
                .Include(p => p.InMtProCategorieFkyNavigation)
                .GroupBy(p => p.InMtProCategorieFkyNavigation != null ?
                    p.InMtProCategorieFkyNavigation.SvMtCatName : "Sin Categoría")
                .Select(
                    group => new
                    {
                        Category = group.Key,
                        Products = group.Select(p => new
                        {
                            ID = p.InMtProPky,
                            Name = p.SvMtProName,
                            Description = p.SvMtProDescription,
                            Price = p.DeMtProPrice,
                        })
                    })
                .ToList();
            return Json(products);
        }

        [HttpPost]
        public IActionResult PostOrder(int table, int productId, int cuantity)
        {
            string? username = HttpContext.Session.GetString("username");

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                bool result = _mainServices.postOrder("NEW", table, productId, cuantity);

                if (result)
                {
                    return Ok(new { message = "Orden creada con éxito" });
                }
                else
                {
                    return BadRequest(new { message = "Error al crear la orden" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

    }
}
