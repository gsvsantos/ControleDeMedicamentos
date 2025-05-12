using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.ConsoleApp.Controllers;

[Route("fornecedor")]
public class ControladorFornecedor : Controller
{
    public IActionResult Menu()
    {
        return View("Menu");
    }

    [HttpGet("cadastrar")]
    public IActionResult FormularioCadastrar()
    {
        return View("Cadastrar");
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(
        [FromForm] string nome,
        [FromForm] string telefone,
        [FromForm] string cnpj)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);

        Fornecedor fornecedor = new Fornecedor(nome, telefone, cnpj);

        repositorioFornecedor.CadastrarRegistro(fornecedor);

        ViewBagHelper.DefinirDados(ViewBag, "Fornecedores", "fornecedor", "cadastrado", fornecedor.Nome);

        return View("Notificacao");
    }

    [HttpGet("visualizar")]
    public IActionResult VisualizarCadastros()
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);

        ViewBag.Fornecedores = repositorioFornecedor.SelecionarRegistros();

        return View("Visualizar");
    }

    [HttpGet("editar/{id:int}")]
    public IActionResult FormularioEditar([FromRoute] int id) // exemplo com [FromRoute], nao precisa pois refere ao id da rota "{excluir/id:int}"
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);

        ViewBag.Fornecedor = repositorioFornecedor.SelecionarRegistroPorId(id);

        return View("Editar");
    }

    [HttpPost("editar/{id:int}")]
    public IActionResult Editar(
        [FromRoute] int id, // exemplo com [FromRoute], nao precisa pois refere ao id da rota "{excluir/id:int}"
        [FromForm] string nome,
        [FromForm] string telefone,
        [FromForm] string cnpj)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);

        Fornecedor fornecedorAtualizado = new Fornecedor(nome, telefone, cnpj);

        repositorioFornecedor.EditarRegistro(id, fornecedorAtualizado);

        ViewBagHelper.DefinirDados(ViewBag, "Fornecedores", "fornecedor", "editado", fornecedorAtualizado.Nome);

        return View("Notificacao");
    }

    [HttpGet("excluir/{id:int}")]
    public IActionResult FormularioExcluir(int id) // exemplo sem [FromRoute], nao precisa pois refere ao id da rota "{excluir/id:int}"
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);

        ViewBag.Fornecedor = repositorioFornecedor.SelecionarRegistroPorId(id);

        return View("Excluir");
    }

    [HttpPost("excluir/{id:int}")]
    public IActionResult Excluir(int id) // exemplo sem [FromRoute], nao precisa pois refere ao id da rota "{excluir/id:int}"
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contexto);

        repositorioFornecedor.ExcluirRegistro(id);

        ViewBagHelper.DefinirDados(ViewBag, "Fornecedores", "fornecedor", "excluído");

        return View("Notificacao");
    }
}