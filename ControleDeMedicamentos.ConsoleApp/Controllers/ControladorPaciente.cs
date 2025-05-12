using ControleDeMedicamentos.ConsoleApp.Compartilhado;
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
        return View("Cadastrar");
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(
        [FromForm] string nome,
        [FromForm] string telefone,
        [FromForm] string cartaoSus)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);
        Paciente paciente = new Paciente(nome, telefone, cartaoSus);

        repositorioPaciente.CadastrarRegistro(paciente);

        ViewBag.Contexto = "Pacientes";
        ViewBag.Tipo = "paciente";
        ViewBag.Mensagem = $"O registro \"{paciente.Nome}\" foi cadastrado com sucesso!";

        return View("Notificacao");
    }

    [HttpGet("visualizar")]
    public IActionResult VisualizarCadastros()
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);

        ViewBag.Pacientes = repositorioPaciente.SelecionarRegistros();

        return View("Visualizar");
    }

    [HttpGet("editar/{id:int}")]
    public IActionResult FormularioEditar(int id)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);

        ViewBag.Paciente = repositorioPaciente.SelecionarRegistroPorId(id);

        return View("Editar");
    }

    [HttpPost("editar/{id:int}")]
    public IActionResult Editar(
        int id,
        [FromForm] string nome,
        [FromForm] string telefone,
        [FromForm] string cartaoSus)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);

        Paciente pacienteAtualizado = new Paciente(nome, telefone, cartaoSus);

        repositorioPaciente.EditarRegistro(id, pacienteAtualizado);

        ViewBag.Contexto = "Pacientes";
        ViewBag.Tipo = "paciente";
        ViewBag.Mensagem = $"O registro \"{pacienteAtualizado.Nome}\" foi editado com sucesso!";

        return View("Notificacao");
    }

    [HttpGet("excluir/{id:int}")]
    public IActionResult FormularioExcluir(int id)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);

        ViewBag.Paciente = repositorioPaciente.SelecionarRegistroPorId(id);

        return View("Excluir");
    }

    [HttpPost("excluir/{id:int}")]
    public IActionResult Excluir(int id)
    {
        ContextoDados contexto = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPacienteEmArquivo(contexto);

        repositorioPaciente.ExcluirRegistro(id);

        ViewBag.Contexto = "Pacientes";
        ViewBag.Tipo = "paciente";
        ViewBag.Mensagem = $"Registro excluído com sucesso!";

        return View("Notificacao");
    }
}
