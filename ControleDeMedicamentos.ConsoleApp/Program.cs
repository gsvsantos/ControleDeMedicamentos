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

        builder.Services.AddControllers();

        WebApplication app = builder.Build();

        WebAppFornecedor webAppFornecedor = new WebAppFornecedor();
        WebAppFuncionario webAppFuncionario = new WebAppFuncionario();
        WebAppPaciente webAppPaciente = new WebAppPaciente();

        webAppFornecedor.Carregar(app);

        webAppFuncionario.Carregar(app);

        webAppPaciente.Carregar(app);

        app.MapControllers();

        app.Run();
    }
}