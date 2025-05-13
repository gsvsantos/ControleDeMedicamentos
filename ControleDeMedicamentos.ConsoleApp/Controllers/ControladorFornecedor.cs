using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.Models;
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
        CadastrarFornecedorViewModel cadastrarVM = new CadastrarFornecedorViewModel();

        return View("Cadastrar");
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarFornecedorViewModel cadastrarVM)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contextoDados);

        Fornecedor novoFornecedor = cadastrarVM.ParaEntidade();

        repositorioFornecedor.CadastrarRegistro(novoFornecedor);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel("Gestão de Fornecedores", "fornecedor",
            $"O registro \"{novoFornecedor.Nome}\" foi cadastrado com sucesso!");

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("visualizar")]
    public IActionResult VisualizarCadastros()
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contextoDados);

        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarRegistros();

        VisualizarFornecedorViewModel visualizarVM = new VisualizarFornecedorViewModel(fornecedores);

        return View("Visualizar", visualizarVM);
    }

    [HttpGet("editar/{id:int}")]
    public IActionResult FormularioEditar([FromRoute] int id) // exemplo com [FromRoute], nao precisa pois refere ao id da rota "{excluir/id:int}"
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contextoDados);

        Fornecedor fornecedorSelecionado = repositorioFornecedor.SelecionarRegistroPorId(id);

        EditarFornecedorViewModel editarVM = new EditarFornecedorViewModel(
            id, fornecedorSelecionado.Nome!, fornecedorSelecionado.Telefone!,
            fornecedorSelecionado.CNPJ!);

        return View("Editar", editarVM);
    }

    [HttpPost("editar/{id:int}")]
    public IActionResult Editar([FromRoute] int id, EditarFornecedorViewModel editarVM) // exemplo com [FromRoute], nao precisa pois refere ao id da rota "{excluir/id:int}"
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contextoDados);

        Fornecedor fornecedorAtualizado = editarVM.ParaEntidade();

        repositorioFornecedor.EditarRegistro(id, fornecedorAtualizado);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel("Gestão de Fornecedores", "fornecedor",
            $"O registro \"{fornecedorAtualizado.Nome}\" foi editado com sucesso!");

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("excluir/{id:int}")]
    public IActionResult FormularioExcluir(int id) // exemplo sem [FromRoute], nao precisa pois refere ao id da rota "{excluir/id:int}"
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contextoDados);

        Fornecedor fornecedorSelecionado = repositorioFornecedor.SelecionarRegistroPorId(id);

        ExcluirFornecedorViewModel excluirVM = new ExcluirFornecedorViewModel(
            id, fornecedorSelecionado.Nome!);

        return View("Excluir", excluirVM);
    }

    [HttpPost("excluir/{id:int}")]
    public IActionResult Excluir(int id) // exemplo sem [FromRoute], nao precisa pois refere ao id da rota "{excluir/id:int}"
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedorEmArquivo(contextoDados);

        repositorioFornecedor.ExcluirRegistro(id);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel("Gestão de Fornecedores", "fornecedor",
            $"Registro excluído com sucesso!");

        return View("Notificacao", notificacaoVM);
    }
}