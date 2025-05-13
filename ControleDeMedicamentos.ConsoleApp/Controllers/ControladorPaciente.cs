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
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contextoDados);

        Paciente novoPaciente = cadastrarVM.ParaEntidade();

        repositorioPaciente.CadastrarRegistro(novoPaciente);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel("Gestão de Pacientes", "paciente",
            $"O registro \"{novoPaciente.Nome}\" foi cadastrado com sucesso!");

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("visualizar")]
    public IActionResult VisualizarCadastros()
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contextoDados);

        List<Paciente> pacientes = repositorioPaciente.SelecionarRegistros();

        VisualizarPacienteViewModel visualizarVM = new VisualizarPacienteViewModel(pacientes);

        return View("Visualizar", visualizarVM);
    }

    [HttpGet("editar/{id:int}")]
    public IActionResult FormularioEditar(int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contextoDados);

        Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

        EditarPacienteViewModel editarVM = new EditarPacienteViewModel(
            id, pacienteSelecionado.Nome!, pacienteSelecionado.Telefone!,
            pacienteSelecionado.CartaoSus!);

        return View("Editar", editarVM);
    }

    [HttpPost("editar/{id:int}")]
    public IActionResult Editar(int id, EditarPacienteViewModel editarVM)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contextoDados);

        Paciente pacienteAtualizado = editarVM.ParaEntidade();

        repositorioPaciente.EditarRegistro(id, pacienteAtualizado);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel("Gestão de Pacientes", "paciente",
            $"O registro \"{pacienteAtualizado.Nome}\" foi editado com sucesso!");

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("excluir/{id:int}")]
    public IActionResult FormularioExcluir(int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contextoDados);

        Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

        ExcluirPacienteViewModel excluirVM = new ExcluirPacienteViewModel(
            id, pacienteSelecionado.Nome!);

        return View("Excluir", excluirVM);
    }

    [HttpPost("excluir/{id:int}")]
    public IActionResult Excluir(int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contextoDados);

        repositorioPaciente.ExcluirRegistro(id);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel("Gestão de Pacientes", "paciente",
            $"Registro excluído com sucesso!");

        return View("Notificacao", notificacaoVM);
    }
}
