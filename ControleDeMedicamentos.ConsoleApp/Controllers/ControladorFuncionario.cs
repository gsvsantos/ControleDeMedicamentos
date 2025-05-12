using ControleDeMedicamentos.ConsoleApp.Compartilhado;
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
        return View("Cadastrar");
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(
        [FromForm] string nome,
        [FromForm] string telefone,
        [FromForm] string cpf)
    {
        ContextoDados contextodados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextodados);

        Funcionario funcionario = new Funcionario(nome, telefone, cpf);

        repositorioFuncionario.CadastrarRegistro(funcionario);

        ViewBag.Contexto = "Funcionários";
        ViewBag.Tipo = "funcionario";
        ViewBag.Mensagem = $"O registro \"{funcionario.Nome}\" foi cadastrado com sucesso!";

        return View("Notificacao");
    }

    [HttpGet("visualizar")]
    public IActionResult VisualizarCadastros()
    {
        ContextoDados contextodados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextodados);

        ViewBag.Funcionarios = repositorioFuncionario.SelecionarRegistros();

        return View("Visualizar");
    }

    [HttpGet("editar/{id:int}")]
    public IActionResult FormularioEditar(int id)
    {
        ContextoDados contextodados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextodados);

        ViewBag.Funcionario = repositorioFuncionario.SelecionarRegistroPorId(id);

        return View("Editar");
    }

    [HttpPost("editar/{id:int}")]
    public IActionResult Editar(
        int id,
        [FromForm] string nome,
        [FromForm] string telefone,
        [FromForm] string cpf)
    {
        ContextoDados contextodados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextodados);

        Funcionario funcionarioAtualizado = new Funcionario(nome, telefone, cpf);

        repositorioFuncionario.EditarRegistro(id, funcionarioAtualizado);

        ViewBag.Contexto = "Funcionários";
        ViewBag.Tipo = "funcionario";
        ViewBag.Mensagem = $"O registro \"{funcionarioAtualizado.Nome}\" foi editado com sucesso!";

        return View("Notificacao");
    }

    [HttpGet("excluir/{id:int}")]
    public IActionResult FormularioExcluir(int id)
    {
        ContextoDados contextodados = new ContextoDados(true);
        IRepositorioFuncionario repositorio = new RepositorioFuncionarioEmArquivo(contextodados);

        ViewBag.Funcionario = repositorio.SelecionarRegistroPorId(id);

        return View("Excluir");
    }

    [HttpPost("excluir/{id:int}")]
    public IActionResult Excluir(int id)
    {
        ContextoDados contextodados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextodados);

        Funcionario funcionarioSelecionado = repositorioFuncionario.SelecionarRegistroPorId(id);

        repositorioFuncionario.ExcluirRegistro(id);

        ViewBag.Contexto = "Funcionários";
        ViewBag.Tipo = "funcionario";
        ViewBag.Mensagem = $"Registro excluído com sucesso!";

        return View("Notificacao");
    }
}
