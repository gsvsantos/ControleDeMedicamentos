using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.Models;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.ConsoleApp.Controllers;

[Route("paciente")]
public class ControladorPaciente : Controller
{
    public IActionResult Menu()
    {
        return View("Menu");
    }

    [HttpGet("cadastrar")]
    public IActionResult FormularioCadastrar()
    {
        CadastrarPacienteViewModel cadastrarVM = new CadastrarPacienteViewModel();

        return View("Cadastrar");
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarPacienteViewModel cadastrarVM)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);

        Paciente novoPaciente = cadastrarVM.ParaEntidade();

        repositorioPaciente.CadastrarRegistro(novoPaciente);

        ViewBagHelper.DefinirDados(ViewBag, "Pacientes", "paciente", "cadastrado", novoPaciente.Nome);

        return View("Notificacao");
    }

    [HttpGet("visualizar")]
    public IActionResult VisualizarCadastros()
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);

        List<Paciente> pacientes = repositorioPaciente.SelecionarRegistros();

        VisualizarPacienteViewModel visualizarVM = new(pacientes);

        return View("Visualizar", visualizarVM);
    }

    [HttpGet("editar/{id:int}")]
    public IActionResult FormularioEditar(int id)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);

        Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

        EditarPacienteViewModel editarVM = new(
            id, pacienteSelecionado.Nome!, pacienteSelecionado.Telefone!,
            pacienteSelecionado.CartaoSus!);

        return View("Editar", editarVM);
    }

    [HttpPost("editar/{id:int}")]
    public IActionResult Editar(int id, EditarPacienteViewModel editarVM)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);

        Paciente pacienteAtualizado = editarVM.ParaEntidade();

        repositorioPaciente.EditarRegistro(id, pacienteAtualizado);

        ViewBagHelper.DefinirDados(ViewBag, "Pacientes", "paciente", "editado", pacienteAtualizado.Nome);

        return View("Notificacao");
    }

    [HttpGet("excluir/{id:int}")]
    public IActionResult FormularioExcluir(int id)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);

        Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

        ExcluirPacienteViewModel excluirVM = new(
            id, pacienteSelecionado.Nome!);

        return View("Excluir", excluirVM);
    }

    [HttpPost("excluir/{id:int}")]
    public IActionResult Excluir(int id)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);

        repositorioPaciente.ExcluirRegistro(id);

        ViewBagHelper.DefinirDados(ViewBag, "Pacientes", "paciente", "excluído");

        return View("Notificacao");
    }
}
