using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

namespace ControleDeMedicamentos.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        // fornecedor, funcionário e paciente.

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        WebApplication app = builder.Build();

        WebAppFuncionario webAppFuncionario = new WebAppFuncionario();
        WebAppPaciente webAppPaciente = new WebAppPaciente();
        webAppFuncionario.Carregar(app);
        webAppPaciente.Carregar(app);

        app.UseRouting();
        app.MapControllers();

        app.Run();
    }
}