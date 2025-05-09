using System.Text;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;

public class WebAppFuncionario
{
    public void Carregar(WebApplication app)
    {
        app.MapGet("/funcionario", Menu);
        app.MapGet("/funcionario/cadastrar", FormularioCadastrar);
        app.MapPost("/funcionario/cadastrar", Cadastrar);
        app.MapGet("/funcionario/editar/{id:int}", FormularioEditar);
        app.MapPost("/funcionario/editar/{id:int}", Editar);
        app.MapGet("/funcionario/excluir/{id:int}", FormularioExcluir);
        app.MapPost("/funcionario/excluir/{id:int}", Excluir);
        app.MapGet("/funcionario/visualizar", VisualizarCadastros);
    }

    private Task Menu(HttpContext context)
    {
        string temp = File.ReadAllText("ModuloFuncionario/Html/Menu.html");
        
        return context.Response.WriteAsync(temp);
    }

    private Task FormularioCadastrar(HttpContext context)
    {
        string temp = File.ReadAllText("ModuloFuncionario/Html/Cadastrar.html");
        
        return context.Response.WriteAsync(temp);
    }

    private Task Cadastrar(HttpContext context)
    {
        ContextoDados contextodados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextodados);
        
        string nome = context.Request.Form["nome"].ToString();
        string telefone = context.Request.Form["telefone"].ToString();
        string cpf = context.Request.Form["cpf"].ToString();
        
        Funcionario funcionario = new Funcionario(nome, telefone, cpf);
        
        repositorioFuncionario.CadastrarRegistro(funcionario);
        
        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");
        
        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#contexto#", "Funcionários");
        sb.Replace("#tipo#", "funcionario");
        sb.Replace("#mensagem#", $"O registro \"{funcionario.Nome}\" foi cadastrado com sucesso!");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }
    
    private Task VisualizarCadastros(HttpContext context)
    {
        ContextoDados contextodados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextodados);

        List<Funcionario> funcionarios = repositorioFuncionario.SelecionarRegistros();

        string conteudo = File.ReadAllText("ModuloFuncionario/Html/Visualizar.html");
        
        StringBuilder sb = new StringBuilder(conteudo);

        foreach (Funcionario f in funcionarios)
        {
            string itemLista =
                $"<li>{f} <a href=\"/funcionario/editar/{f.Id}\">Editar</a> <a href=\"/funcionario/excluir/{f.Id}\">Excluir</a> </li> #funcionario#";
            
            sb.Replace("#funcionario#", itemLista);
        }

        sb.Replace("#funcionario#", "");

        string conteudoString = sb.ToString();
        
        return context.Response.WriteAsync(conteudoString);
    }

    private Task FormularioEditar(HttpContext context)
    {
        ContextoDados contextodados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextodados);
        
        int id = Convert.ToInt32(context.GetRouteValue("id"));
        
        Funcionario funcionarioSelecionado = repositorioFuncionario.SelecionarRegistroPorId(id);
        
        string form = File.ReadAllText("ModuloFuncionario/Html/Editar.html");
        
        StringBuilder sb = new StringBuilder(form);
        
        sb.Replace("#id#", id.ToString());
        sb.Replace("#nome#", funcionarioSelecionado.Nome);
        sb.Replace("#telefone#", funcionarioSelecionado.Telefone);
        sb.Replace("#cpf#", funcionarioSelecionado.CPF);
        
        string conteudoString = sb.ToString();
        
        return context.Response.WriteAsync(conteudoString);        
    }

    private Task Editar(HttpContext context)
    {        
        ContextoDados contextodados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextodados);
        
        int id = Convert.ToInt32(context.GetRouteValue("id"));
        
        string nome = context.Request.Form["nome"].ToString();
        string telefone = context.Request.Form["telefone"].ToString();
        string cpf = context.Request.Form["cpf"].ToString();
        
        Funcionario funcionarioAtualizado = new Funcionario(nome, telefone, cpf);

        repositorioFuncionario.EditarRegistro(id, funcionarioAtualizado);
        
        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");
        
        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#contexto#", "Funcionários");
        sb.Replace("#tipo#", "funcionario");
        sb.Replace("#mensagem#", $"O registro #{id} - \"{funcionarioAtualizado.Nome}\" foi editado com sucesso!");
        
        string conteudoString = sb.ToString();
        
        return context.Response.WriteAsync(conteudoString);
    }

    private Task FormularioExcluir(HttpContext context)
    {
        ContextoDados contextodados = new ContextoDados(true);
        IRepositorioFuncionario repositorio = new RepositorioFuncionarioEmArquivo(contextodados);
        
        int id = Convert.ToInt32(context.GetRouteValue("id"));
        
        Funcionario funcionarioSelecionado = repositorio.SelecionarRegistroPorId(id);

        string form = File.ReadAllText("ModuloFuncionario/Html/Excluir.html");
        
        StringBuilder sb = new StringBuilder(form);

        sb.Replace("#id#", id.ToString());
        sb.Replace("#funcionario#", funcionarioSelecionado.Nome);
        
        string conteudoString = sb.ToString();
        
        return context.Response.WriteAsync(conteudoString);
    }

    private Task Excluir(HttpContext context)
    {
        ContextoDados contextodados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextodados);
        
        int id = Convert.ToInt32(context.GetRouteValue("id"));
        
        Funcionario funcionarioSelecionado = repositorioFuncionario.SelecionarRegistroPorId(id);
        
        repositorioFuncionario.ExcluirRegistro(id);
        
        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");
        
        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#contexto", "Funcionários");
        sb.Replace("#tipo#", "funcionario");
        sb.Replace("#mensagem#", $"O registro foi excluído com sucesso!");
        
        string conteudoString = sb.ToString();
        
        return context.Response.WriteAsync(conteudoString);
    }
}