using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.Controllers;

[Route("/")]
public class ControladorPaginaInicial : Controller
{
    public IActionResult PaginaInicial()
    {
        return View("PaginaInicial");
    }
}
