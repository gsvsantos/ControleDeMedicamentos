using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Models;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoEntrada;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.ConsoleApp.Controllers;

[Route("requisicoes-entrada")]
public class ControladorRequisicaoEntrada : Controller
{
    private ContextoDados contextoDados;
    private IRepositorioFuncionario repositorioFuncionario;
    private IRepositorioMedicamento repositorioMedicamento;
    private IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada;

    public ControladorRequisicaoEntrada()
    {
        contextoDados = new ContextoDados(true);
        repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextoDados);
        repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contextoDados);
        repositorioRequisicaoEntrada = new RepositorioRequisicaoEntradaEmArquivo(contextoDados);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        List<Funcionario> funcionarios = repositorioFuncionario.SelecionarRegistros();
        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarRegistros();

        CadastrarRequisicaoEntradaViewModel cadastrarVM = new(funcionarios, medicamentos);

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarRequisicaoEntradaViewModel cadastarVM)
    {
        Funcionario funcionarioSelecionado = repositorioFuncionario.SelecionarRegistroPorId(cadastarVM.FuncionarioId);
        Medicamento medicamentoSelecionado = repositorioMedicamento.SelecionarRegistroPorId(cadastarVM.MedicamentoId);

        RequisicaoEntrada novaRequisicaoEntrada = new(
            medicamentoSelecionado,
            funcionarioSelecionado,
            cadastarVM.QuantidadeMedicamento);

        repositorioRequisicaoEntrada.CadastrarRegistro(novaRequisicaoEntrada);
        repositorioMedicamento.AdicionarEstoque(medicamentoSelecionado, novaRequisicaoEntrada.QuantidadeMedicamento);

        NotificacaoViewModel notificacaoVM = new(
            "Gestão de Requisição de Entrada",
            "requisicoes-entrada",
            $"Requisição feita com sucesso!\"");

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        List<RequisicaoEntrada> requisicoesEntrada = repositorioRequisicaoEntrada.SelecionarRegistros();

        VisualizarRequisicaoEntradaViewModel visualizarVM = new(requisicoesEntrada);

        return View(visualizarVM);
    }
}
