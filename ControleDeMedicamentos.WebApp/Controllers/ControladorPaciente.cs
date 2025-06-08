using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Extensions;
using ControleDeMedicamentos.WebApp.Models;
using ControleDeMedicamentos.WebApp.ModuloPaciente;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.Controllers;

[Route("pacientes")]
public class ControladorPaciente : Controller
{
    private readonly ContextoDados contextoDados;
    private readonly IRepositorioPaciente repositorioPaciente;

    public ControladorPaciente()
    {
        contextoDados = new(true);
        repositorioPaciente = new RepositorioPacienteEmArquivo(contextoDados);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        CadastrarPacienteViewModel cadastrarVM = new();

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarPacienteViewModel cadastrarVM, string btnSubmit)
    {
        if (btnSubmit == "voltar")
        {
            return RedirectToAction("Visualizar");
        }
        else
        {
            Paciente novoPaciente = cadastrarVM.ParaEntidade();

            repositorioPaciente.CadastrarRegistro(novoPaciente);

            NotificacaoViewModel notificacaoVM = new(
                "Gestão de Pacientes",
                "pacientes",
                $"O registro \"{novoPaciente.Nome}\" foi cadastrado com sucesso!");

            return View("Notificacao", notificacaoVM);
        }
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        List<Paciente> pacientes = repositorioPaciente.SelecionarRegistros();

        VisualizarPacienteViewModel visualizarVM = new(pacientes);

        return View(visualizarVM);
    }

    [HttpGet("editar/{id:Guid}")]
    public IActionResult Editar(Guid id)
    {
        Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

        EditarPacienteViewModel editarVM = new(
            id, pacienteSelecionado.Nome!, pacienteSelecionado.Telefone!,
            pacienteSelecionado.CartaoSUS!);

        return View(editarVM);
    }

    [HttpPost("editar/{id:Guid}")]
    public IActionResult Editar(Guid id, EditarPacienteViewModel editarVM, string btnSubmit)
    {
        if (btnSubmit == "cancelar")
        {
            return RedirectToAction("Visualizar");
        }
        else
        {
            Paciente pacienteAtualizado = editarVM.ParaEntidade();

            repositorioPaciente.EditarRegistro(id, pacienteAtualizado);

            NotificacaoViewModel notificacaoVM = new(
                "Gestão de Pacientes",
                "pacientes",
                $"O registro \"{pacienteAtualizado.Nome}\" foi editado com sucesso!");

            return View("Notificacao", notificacaoVM);
        }
    }

    [HttpGet("excluir/{id:Guid}")]
    public IActionResult Excluir(Guid id)
    {
        Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

        ExcluirPacienteViewModel excluirVM = new(
            id, pacienteSelecionado.Nome!);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:Guid}")]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        repositorioPaciente.ExcluirRegistro(id);

        NotificacaoViewModel notificacaoVM = new(
            "Gestão de Pacientes",
            "pacientes",
            $"Registro excluído com sucesso!");

        return View("Notificacao", notificacaoVM);
    }

    [HttpPost("excluirmultiplo")]
    public IActionResult ExcluirMultiplo([FromBody] List<Guid> idsSelecionados)
    {
        foreach (Guid id in idsSelecionados)
        {
            repositorioPaciente.ExcluirRegistro(id);
        }

        return RedirectToAction("Visualizar");
    }
}
