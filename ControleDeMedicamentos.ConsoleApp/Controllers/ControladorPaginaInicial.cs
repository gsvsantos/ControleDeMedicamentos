using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.ConsoleApp.Controllers;

[Route("/")]
public class ControladorPaginaInicial : Controller
{
    public IActionResult PaginaInicial()
    {
        return View("PaginaInicial");
    }
}
