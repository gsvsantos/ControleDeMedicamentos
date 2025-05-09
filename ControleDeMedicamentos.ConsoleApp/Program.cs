using System.Text;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;

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

        ContextoDados contexto = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);

        app.MapGet("/", PaginaInicial);
        webAppFornecedor.Carregar(app);
        webAppFuncionario.Carregar(app);

        app.Run();
    }
    private static Task PaginaInicial(HttpContext context)
    {
        string temp = File.ReadAllText("Compartilhado/Html/PaginaInicial.html");

        return context.Response.WriteAsync(temp);
    }
}