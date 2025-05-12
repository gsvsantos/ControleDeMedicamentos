using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.ConsoleApp.Controllers;

[Route("/")]
public class ControladorPaginaInicial : Controller
{
    public IActionResult PaginaInicial()
    {
        string temp = System.IO.File.ReadAllText("Compartilhado/Html/PaginaInicial.html");

        return Content(temp, "text/html");
    }
}
