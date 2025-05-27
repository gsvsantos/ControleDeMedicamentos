using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.Models;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.ConsoleApp.Controllers;

[Route("funcionarios")]
public class ControladorFuncionario : Controller
{
    private readonly ContextoDados contextoDados;
    private readonly IRepositorioFuncionario repositorioFuncionario;

    public ControladorFuncionario()
    {
        contextoDados = new ContextoDados(true);
        repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextoDados);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        CadastrarFuncionarioViewModel cadastrarVM = new CadastrarFuncionarioViewModel();

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarFuncionarioViewModel cadastrarVM)
    {
        Funcionario novoFuncionario = cadastrarVM.ParaEntidade();

        repositorioFuncionario.CadastrarRegistro(novoFuncionario);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
            "Gestão de Funcionários",
            "funcionarios",
            $"O registro \"{novoFuncionario.Nome}\" foi cadastrado com sucesso!");

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        List<Funcionario> funcionarios = repositorioFuncionario.SelecionarRegistros();

        VisualizarFuncionarioViewModel visualizarVM = new VisualizarFuncionarioViewModel(funcionarios);

        return View(visualizarVM);
    }

    [HttpGet("editar/{id:Guid}")]
    public IActionResult Editar(Guid id)
    {
        Funcionario funcionarioSelecionado = repositorioFuncionario.SelecionarRegistroPorId(id);

        EditarFuncionarioViewModel editarVM = new EditarFuncionarioViewModel(
            id, funcionarioSelecionado.Nome!, funcionarioSelecionado.Telefone!,
            funcionarioSelecionado.CPF!);

        return View(editarVM);
    }

    [HttpPost("editar/{id:Guid}")]
    public IActionResult Editar(Guid id, EditarFuncionarioViewModel editarVM)
    {
        Funcionario funcionarioAtualizado = editarVM.ParaEntidade();

        repositorioFuncionario.EditarRegistro(id, funcionarioAtualizado);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
            "Gestão de Funcionários",
            "funcionarios",
            $"O registro \"{funcionarioAtualizado.Nome}\" foi editado com sucesso!");

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("excluir/{id:Guid}")]
    public IActionResult Excluir(Guid id)
    {
        Funcionario funcionarioSelecionado = repositorioFuncionario.SelecionarRegistroPorId(id);

        ExcluirFuncionarioViewModel excluirVM = new ExcluirFuncionarioViewModel(
            id, funcionarioSelecionado.Nome!);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:Guid}")]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        repositorioFuncionario.ExcluirRegistro(id);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
            "Gestão de Funcionários",
            "funcionarios",
            $"Registro excluído com sucesso!");

        return View("Notificacao", notificacaoVM);
    }
}
