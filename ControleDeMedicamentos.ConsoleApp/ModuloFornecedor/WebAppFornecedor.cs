using System.Text;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;

public class WebAppFornecedor
{
    public void Carregar(WebApplication app)
    {
        app.MapGet("/fornecedor", Menu);
        app.MapGet("/fornecedor/cadastrar", FormularioCadastrar);
        app.MapPost("/fornecedor/cadastrar", Cadastrar);
        app.MapGet("/fornecedor/editar/{id:int}", FormularioEditar);
        app.MapPost("/fornecedor/editar/{id:int}", Editar);
        app.MapGet("/fornecedor/excluir/{id:int}", FormularioExcluir);
        app.MapPost("/fornecedor/excluir/{id:int}", Excluir);
        app.MapGet("/fornecedor/visualizar", VisualizarCadastros);
    }

    private Task Menu(HttpContext context)
    {
        string temp = File.ReadAllText("ModuloFornecedor/Html/Menu.html");
        
        return context.Response.WriteAsync(temp);
    }

    private Task FormularioCadastrar(HttpContext context)
    {
        string temp = File.ReadAllText("ModuloFornecedor/Html/Cadastrar.html");

        return context.Response.WriteAsync(temp);
    }

    private Task Cadastrar(HttpContext context)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);

        string nome = context.Request.Form["nome"].ToString();
        string telefone = context.Request.Form["telefone"].ToString();
        string cnpj = context.Request.Form["cnpj"].ToString();

        Fornecedor fornecedor = new Fornecedor(nome, telefone, cnpj);

        repositorioFornecedor.CadastrarRegistro(fornecedor);

        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#contexto#", "Fornecedores");
        sb.Replace("#tipo#", "fornecedor");
        sb.Replace("#mensagem#", $"O registro \"{fornecedor.Nome}\" foi cadastrado com sucesso!");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    private Task VisualizarCadastros(HttpContext context)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);

        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarRegistros();

        string conteudo = File.ReadAllText("ModuloFornecedor/Html/Visualizar.html");

        StringBuilder sb = new StringBuilder(conteudo);

        foreach (Fornecedor f in fornecedores)
        {
            string itemLista =
                $"<li>{f} <a href=\"/fornecedor/editar/{f.Id}\">Editar</a> <a href=\"/fornecedor/excluir/{f.Id}\">Excluir</a> </li> #fornecedor#";

            sb.Replace("#fornecedor#", itemLista);
        }

        sb.Replace("#fornecedor#", "");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    private Task FormularioEditar(HttpContext context)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);
        
        int id = Convert.ToInt32(context.GetRouteValue("id"));

        Fornecedor fornecedorSelecionado = repositorioFornecedor.SelecionarRegistroPorId(id);
        
        string form = File.ReadAllText("ModuloFornecedor/Html/Editar.html");
        
        StringBuilder sb = new StringBuilder(form);

        sb.Replace("#id#", id.ToString());
        sb.Replace("#nome#", fornecedorSelecionado.Nome);
        sb.Replace("#telefone#", fornecedorSelecionado.Telefone);
        sb.Replace("#cnpj#", fornecedorSelecionado.CNPJ);
        
        string conteudoString = sb.ToString();
        
        return context.Response.WriteAsync(conteudoString);
    }

    private Task Editar(HttpContext context)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);
        
        int id = Convert.ToInt32(context.GetRouteValue("id"));
        
        string nome = context.Request.Form["nome"].ToString();
        string telefone = context.Request.Form["telefone"].ToString();
        string cnpj = context.Request.Form["cnpj"].ToString();

        Fornecedor fornecedorAtualizado = new Fornecedor(nome, telefone, cnpj);

        repositorioFornecedor.EditarRegistro(id, fornecedorAtualizado);

        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#contexto#", "Fornecedores");
        sb.Replace("#tipo#", "fornecedor");
        sb.Replace("#mensagem#", $"O registro #{id} - \"{fornecedorAtualizado.Nome}\" foi editado com sucesso!");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    private Task FormularioExcluir(HttpContext context)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);
        
        int id = Convert.ToInt32(context.GetRouteValue("id"));

        Fornecedor fornecedorSelecionado = repositorioFornecedor.SelecionarRegistroPorId(id);
        
        string form = File.ReadAllText("ModuloFornecedor/Html/Excluir.html");
        
        StringBuilder sb = new StringBuilder(form);

        sb.Replace("#id#", id.ToString());
        sb.Replace("#fornecedor#", fornecedorSelecionado.Nome);
        
        string conteudoString = sb.ToString();
        
        return context.Response.WriteAsync(conteudoString);
    }

    private Task Excluir(HttpContext context)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);
        
        int id = Convert.ToInt32(context.GetRouteValue("id"));

        repositorioFornecedor.ExcluirRegistro(id);

        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#contexto#", "Fornecedores");
        sb.Replace("#tipo#", "fornecedor");
        sb.Replace("#mensagem#", $"O registro foi excluído com sucesso!");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }
}