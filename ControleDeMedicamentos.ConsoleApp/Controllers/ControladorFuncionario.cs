using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.Models;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.ConsoleApp.Controllers;

[Route("funcionario")]
public class ControladorFuncionario : Controller
{
    public IActionResult Menu()
    {
        return View("Menu");
    }

    [HttpGet("cadastrar")]
    public IActionResult FormularioCadastrar()
    {
        CadastrarFuncionarioViewModel cadastrarVM = new CadastrarFuncionarioViewModel();

        return View("Cadastrar");
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarFuncionarioViewModel cadastrarVM)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextoDados);

        Funcionario novoFuncionario = cadastrarVM.ParaEntidade();

        repositorioFuncionario.CadastrarRegistro(novoFuncionario);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel("Gestão de Funcionários", "funcionario",
            $"O registro \"{novoFuncionario.Nome}\" foi cadastrado com sucesso!");

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("visualizar")]
    public IActionResult VisualizarCadastros()
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextoDados);

        List<Funcionario> funcionarios = repositorioFuncionario.SelecionarRegistros();

        VisualizarFuncionarioViewModel visualizarVM = new VisualizarFuncionarioViewModel(funcionarios);

        return View("Visualizar", visualizarVM);
    }

    [HttpGet("editar/{id:int}")]
    public IActionResult FormularioEditar(int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextoDados);

        Funcionario funcionarioSelecionado = repositorioFuncionario.SelecionarRegistroPorId(id);

        EditarFuncionarioViewModel editarVM = new EditarFuncionarioViewModel(
            id, funcionarioSelecionado.Nome!, funcionarioSelecionado.Telefone!,
            funcionarioSelecionado.CPF!);

        return View("Editar", editarVM);
    }

    [HttpPost("editar/{id:int}")]
    public IActionResult Editar(int id, EditarFuncionarioViewModel editarVM)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextoDados);

        Funcionario funcionarioAtualizado = editarVM.ParaEntidade();

        repositorioFuncionario.EditarRegistro(id, funcionarioAtualizado);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel("Gestão de Funcionários", "funcionario",
            $"O registro \"{funcionarioAtualizado.Nome}\" foi editado com sucesso!");

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("excluir/{id:int}")]
    public IActionResult FormularioExcluir(int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextoDados);

        Funcionario funcionarioSelecionado = repositorioFuncionario.SelecionarRegistroPorId(id);

        ExcluirFuncionarioViewModel excluirVM = new ExcluirFuncionarioViewModel(
            id, funcionarioSelecionado.Nome!);

        return View("Excluir", excluirVM);
    }

    [HttpPost("excluir/{id:int}")]
    public IActionResult Excluir(int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextoDados);

        repositorioFuncionario.ExcluirRegistro(id);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel("Gestão de Funcionários", "funcionario",
            $"Registro excluído com sucesso!");

        return View("Notificacao", notificacaoVM);
    }
}
