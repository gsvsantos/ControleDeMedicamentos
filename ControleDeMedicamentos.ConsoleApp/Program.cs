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

        WebAppPaciente webAppPaciente = new WebAppPaciente();
        webAppPaciente.Carregar(app);

        app.UseRouting();
        app.MapControllers();

        app.Run();
    }
}