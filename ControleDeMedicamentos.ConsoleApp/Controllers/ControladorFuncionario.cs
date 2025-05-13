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
        CadastrarFuncionarioViewModel cadastarVM = new();

        return View("Cadastrar");
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarFuncionarioViewModel cadastrarVM)
    {
        ContextoDados contextodados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextodados);

        Funcionario funcionario = cadastrarVM.ParaEntidade();

        repositorioFuncionario.CadastrarRegistro(funcionario);

        ViewBagHelper.DefinirDados(ViewBag, "Funcionários", "funcionario", "cadastrado", funcionario.Nome);

        return View("Notificacao");
    }

    [HttpGet("visualizar")]
    public IActionResult VisualizarCadastros()
    {
        ContextoDados contextodados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextodados);

        List<Funcionario> funcionarios = repositorioFuncionario.SelecionarRegistros();

        VisualizarFuncionarioViewModel visualizarVM = new(funcionarios);

        return View("Visualizar", visualizarVM);
    }

    [HttpGet("editar/{id:int}")]
    public IActionResult FormularioEditar(int id)
    {
        ContextoDados contextodados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextodados);

        Funcionario funcionarioSelecionado = repositorioFuncionario.SelecionarRegistroPorId(id);

        EditarFuncionarioViewModel editarVM = new EditarFuncionarioViewModel(
            id, funcionarioSelecionado.Nome!, funcionarioSelecionado.Telefone!,
            funcionarioSelecionado.CPF!);

        return View("Editar", editarVM);
    }

    [HttpPost("editar/{id:int}")]
    public IActionResult Editar(int id, EditarFuncionarioViewModel editarVM)
    {
        ContextoDados contextodados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextodados);

        Funcionario funcionarioAtualizado = editarVM.ParaEntidade();

        repositorioFuncionario.EditarRegistro(id, funcionarioAtualizado);

        ViewBagHelper.DefinirDados(ViewBag, "Funcionários", "funcionario", "editado", funcionarioAtualizado.Nome);

        return View("Notificacao");
    }

    [HttpGet("excluir/{id:int}")]
    public IActionResult FormularioExcluir(int id)
    {
        ContextoDados contextodados = new ContextoDados(true);
        IRepositorioFuncionario repositorio = new RepositorioFuncionarioEmArquivo(contextodados);

        Funcionario funcionarioSelecionado = repositorio.SelecionarRegistroPorId(id);

        ExcluirFuncionarioViewModel excluirVM = new(id, funcionarioSelecionado.Nome!);

        return View("Excluir", excluirVM);
    }

    [HttpPost("excluir/{id:int}")]
    public IActionResult Excluir(int id)
    {
        ContextoDados contextodados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextodados);

        repositorioFuncionario.ExcluirRegistro(id);

        ViewBagHelper.DefinirDados(ViewBag, "Funcionários", "funcionario", "excluído");

        return View("Notificacao");
    }
}
