using System.Text;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;

internal class WebAppFornecedor
{
    public void Carregar(WebApplication app)
    {
        EndpointRouteBuilderExtensions.MapGet((IEndpointRouteBuilder)app, (string)"/fornecedor", (RequestDelegate)MenuFornecedor);
        EndpointRouteBuilderExtensions.MapGet((IEndpointRouteBuilder)app, (string)"/fornecedor/cadastrar", (RequestDelegate)FormularioCadastrarFornecedor);
        EndpointRouteBuilderExtensions.MapPost((IEndpointRouteBuilder)app, (string)"/fornecedor/cadastrar", (RequestDelegate)CadastrarFornecedor);
        EndpointRouteBuilderExtensions.MapGet((IEndpointRouteBuilder)app, (string)"/fornecedor/editar/{id:int}", (RequestDelegate)FormularioEditarFornecedor);
        EndpointRouteBuilderExtensions.MapPost((IEndpointRouteBuilder)app, (string)"/fornecedor/editar/{id:int}", (RequestDelegate)EditarFornecedor);
        EndpointRouteBuilderExtensions.MapGet((IEndpointRouteBuilder)app, (string)"/fornecedor/excluir/{id:int}", (RequestDelegate)FormularioExcluirFornecedor);
        EndpointRouteBuilderExtensions.MapPost((IEndpointRouteBuilder)app, (string)"/fornecedor/excluir/{id:int}", (RequestDelegate)ExcluirFornecedor);
        EndpointRouteBuilderExtensions.MapGet((IEndpointRouteBuilder)app, (string)"/fornecedor/visualizar", (RequestDelegate)VisualizarFornecedores);
    }

    private Task MenuFornecedor(HttpContext context)
    {
        string temp = File.ReadAllText("ModuloFornecedor/Html/Menu.html");
        
        return context.Response.WriteAsync(temp);
    }

    private Task FormularioCadastrarFornecedor(HttpContext context)
    {
        string temp = File.ReadAllText("ModuloFornecedor/Html/Cadastrar.html");

        return context.Response.WriteAsync(temp);
    }

    private Task CadastrarFornecedor(HttpContext context)
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
        sb.Replace("#mensagem#", $"O registro \"{fornecedor.Nome}\" foi cadastrado com sucesso!");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    private Task VisualizarFornecedores(HttpContext context)
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

    private Task FormularioEditarFornecedor(HttpContext context)
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

    private Task EditarFornecedor(HttpContext context)
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
        sb.Replace("#mensagem#", $"O registro #{id} - \"{fornecedorAtualizado.Nome}\" foi editado com sucesso!");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    private Task FormularioExcluirFornecedor(HttpContext context)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);
        
        int id = Convert.ToInt32(context.GetRouteValue("id"));

        Fornecedor fornecedorSelecionado = repositorioFornecedor.SelecionarRegistroPorId(id);
        
        string form = File.ReadAllText("ModuloFornecedor/Html/Excluir.html");
        
        StringBuilder sb = new StringBuilder(form);

        sb.Replace("#id#", id.ToString());
        sb.Replace("#fabricante#", fornecedorSelecionado.Nome);
        
        string conteudoString = sb.ToString();
        
        return context.Response.WriteAsync(conteudoString);
    }

    private Task ExcluirFornecedor(HttpContext context)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);
        
        int id = Convert.ToInt32(context.GetRouteValue("id"));

        repositorioFornecedor.ExcluirRegistro(id);

        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#contexto#", "Fornecedores");
        sb.Replace("#mensagem#", $"O registro foi excluído com sucesso!");

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }
}