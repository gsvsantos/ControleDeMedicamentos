using System.Text;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using CsvHelper;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

public class WebAppPaciente
{
    public void Carregar(WebApplication app)
    {
        app.MapGet("/paciente", Menu);
        app.MapGet("/paciente/cadastrar", FormularioCadastrar);
        app.MapPost("/paciente/cadastrar", Cadastrar);
        app.MapGet("/paciente/visualizar", VisualizarCadastros);
        app.MapGet("/paciente/editar/{id:int}", FormularioEditar);
        app.MapPost("/paciente/editar/{id:int}", Editar);;
        app.MapGet("/paciente/excluir/{id:int}", FormularioExcluir);
        app.MapPost("/paciente/excluir/{id:int}", Excluir);
    }

    private Task Menu(HttpContext context)
    {
        string temp = File.ReadAllText("ModuloPaciente/Html/Menu.html");

        return context.Response.WriteAsync(temp);
    }

    private Task FormularioCadastrar(HttpContext context)
    {
        string temp = File.ReadAllText("ModuloPaciente/Html/Cadastrar.html");

        return context.Response.WriteAsync(temp);
    }

    private Task Cadastrar(HttpContext context)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);

        string nome = context.Request.Form["nome"].ToString();
        string telefone = context.Request.Form["telefone"].ToString();
        string cartaoSus = context.Request.Form["cartaoSus"].ToString();

        Paciente paciente = new Paciente(nome, telefone, cartaoSus);

        repositorioPaciente.CadastrarRegistro(paciente);

        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#contexto#", "Pacientes");
        sb.Replace("#tipo#", "paciente");
        sb.Replace("#mensagem#", $"O registro {paciente.Nome} foi cadastrado com sucesso!");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    private Task VisualizarCadastros(HttpContext context)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);

        List<Paciente> pacientes = repositorioPaciente.SelecionarRegistros();

        string conteudo = File.ReadAllText("ModuloPaciente/Html/Visualizar.html");

        StringBuilder sb = new StringBuilder(conteudo);

        if (pacientes.Count == 0)
            sb.Replace("#paciente#", "Nenhum paciente cadastrado.");

        foreach (Paciente p in pacientes)
        {
            string itemLista =
                $"<li>{p} <a href=\"/paciente/editar/{p.Id}\">Editar</a> <a href=\"/paciente/excluir/{p.Id}\">Excluir</a> </li> #paciente#";

            sb.Replace("#paciente#", itemLista);
        }

        sb.Replace("#paciente#", "");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    private Task FormularioEditar(HttpContext context)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);

        int id = Convert.ToInt32(context.GetRouteValue("id"));

        Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

        string form = File.ReadAllText("ModuloPaciente/Html/Editar.html");

        StringBuilder sb = new StringBuilder(form);

        sb.Replace("#id#", id.ToString());
        sb.Replace("#nome#", pacienteSelecionado.Nome);
        sb.Replace("#telefone#", pacienteSelecionado.Telefone);
        sb.Replace("#cartaoSus#", pacienteSelecionado.CartaoSus);

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    private Task Editar(HttpContext context)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);

        int id = Convert.ToInt32(context.GetRouteValue("id"));

        string nome = context.Request.Form["nome"].ToString();
        string telefone = context.Request.Form["telefone"].ToString();
        string cartaoSus = context.Request.Form["cartaoSus"].ToString();

        Paciente pacienteAtualizado = new Paciente(nome, telefone, cartaoSus);

        repositorioPaciente.EditarRegistro(id, pacienteAtualizado);

        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#contexto#", "Pacientes");
        sb.Replace("#tipo#", "paciente");
        sb.Replace("#mensagem#", $"O registro #{id} - \"{pacienteAtualizado.Nome}\" foi editado com sucesso!");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    private Task FormularioExcluir(HttpContext context)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);

        int id = Convert.ToInt32(context.GetRouteValue("id"));

        Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

        string form = File.ReadAllText("ModuloPaciente/Html/Excluir.html");

        StringBuilder sb = new StringBuilder(form);

        sb.Replace("#id", id.ToString());
        sb.Replace("#paciente#", pacienteSelecionado.Nome);

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    private Task Excluir(HttpContext context)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);

        int id = Convert.ToInt32(context.GetRouteValue("id"));

        repositorioPaciente.ExcluirRegistro(id);

        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#contexto#", "Pacientes");
        sb.Replace("#tipo#", "paciente");
        sb.Replace("#mensagem#", "O registro foi excluído com sucesso!");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }
}