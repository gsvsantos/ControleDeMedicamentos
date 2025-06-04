using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Extensions;
using ControleDeMedicamentos.WebApp.Models;
using ControleDeMedicamentos.WebApp.ModuloFornecedor;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.Controllers;

[Route("fornecedores")]
public class ControladorFornecedor : Controller
{
    private readonly ContextoDados contextoDados;
    private readonly IRepositorioFornecedor repositorioFornecedor;

    public ControladorFornecedor()
    {
        contextoDados = new ContextoDados(true);
        repositorioFornecedor = new RepositorioFornecedorEmArquivo(contextoDados);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        CadastrarFornecedorViewModel cadastrarVM = new CadastrarFornecedorViewModel();

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarFornecedorViewModel cadastrarVM, string btnSubmit)
    {
        if (btnSubmit == "voltar")
        {
            return RedirectToAction("Visualizar");
        }
        else
        {
            Fornecedor novoFornecedor = cadastrarVM.ParaEntidade();

            repositorioFornecedor.CadastrarRegistro(novoFornecedor);

            NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
                "Gestão de Fornecedores",
                "fornecedores",
                $"O registro \"{novoFornecedor.Nome}\" foi cadastrado com sucesso!");

            return View("Notificacao", notificacaoVM);
        }
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarRegistros();

        VisualizarFornecedorViewModel visualizarVM = new VisualizarFornecedorViewModel(fornecedores);

        return View(visualizarVM);
    }

    [HttpGet("editar/{id:Guid}")]
    public IActionResult Editar([FromRoute] Guid id) // exemplo com [FromRoute], nao precisa pois refere ao id da rota "{excluir/id:Guid}"
    {
        Fornecedor fornecedorSelecionado = repositorioFornecedor.SelecionarRegistroPorId(id);

        EditarFornecedorViewModel editarVM = new EditarFornecedorViewModel(
            id, fornecedorSelecionado.Nome!, fornecedorSelecionado.Telefone!,
            fornecedorSelecionado.CNPJ!);

        return View(editarVM);
    }

    [HttpPost("editar/{id:Guid}")]
    public IActionResult Editar([FromRoute] Guid id, EditarFornecedorViewModel editarVM, string btnSubmit) // exemplo com [FromRoute], nao precisa pois refere ao id da rota "{excluir/id:Guid}"
    {
        if (btnSubmit == "cancelar")
        {
            return RedirectToAction("Visualizar");
        }
        else
        {
            Fornecedor fornecedorAtualizado = editarVM.ParaEntidade();

            repositorioFornecedor.EditarRegistro(id, fornecedorAtualizado);

            NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
                "Gestão de Fornecedores",
                "fornecedores",
                $"O registro \"{fornecedorAtualizado.Nome}\" foi editado com sucesso!");

            return View("Notificacao", notificacaoVM);
        }
    }

    [HttpGet("excluir/{id:Guid}")]
    public IActionResult Excluir(Guid id) // exemplo sem [FromRoute], nao precisa pois refere ao id da rota "{excluir/id:Guid}"
    {
        Fornecedor fornecedorSelecionado = repositorioFornecedor.SelecionarRegistroPorId(id);

        ExcluirFornecedorViewModel excluirVM = new ExcluirFornecedorViewModel(
            id, fornecedorSelecionado.Nome!);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:Guid}")]
    public IActionResult ExcluirConfirmado(Guid id) // exemplo sem [FromRoute], nao precisa pois refere ao id da rota "{excluir/id:Guid}"
    {
        repositorioFornecedor.ExcluirRegistro(id);

        NotificacaoViewModel notificacaoVM = new(
            "Gestão de Fornecedores",
            "fornecedores",
            $"Registro excluído com sucesso!");

        return View("Notificacao", notificacaoVM);
    }
}