using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

namespace ControleDeMedicamentos.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        // fornecedor, funcionário e paciente.

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        WebApplication app = builder.Build();

        WebAppFornecedor webAppFornecedor = new WebAppFornecedor();
        WebAppFuncionario webAppFuncionario = new WebAppFuncionario();
        WebAppPaciente webAppPaciente = new WebAppPaciente();

        app.MapGet("/", PaginaInicial);
        webAppFornecedor.Carregar(app);
        webAppFuncionario.Carregar(app);
        webAppPaciente.Carregar(app);

        app.Run();
    }
    private static Task PaginaInicial(HttpContext context)
    {
        string temp = File.ReadAllText("Compartilhado/Html/PaginaInicial.html");

        return context.Response.WriteAsync(temp);
    }
}