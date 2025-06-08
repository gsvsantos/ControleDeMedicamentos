using System.Text.Json;
using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Extensions;
using ControleDeMedicamentos.WebApp.Models;
using ControleDeMedicamentos.WebApp.ModuloFuncionario;
using ControleDeMedicamentos.WebApp.ModuloMedicamento;
using ControleDeMedicamentos.WebApp.ModuloRequisicaoEntrada;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.Controllers;

[Route("requisicoes-entrada")]
public class ControladorRequisicaoEntrada : Controller
{
    private readonly ContextoDados contextoDados;
    private readonly IRepositorioFuncionario repositorioFuncionario;
    private readonly IRepositorioMedicamento repositorioMedicamento;
    private readonly IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada;

    public ControladorRequisicaoEntrada()
    {
        contextoDados = new ContextoDados(true);
        repositorioFuncionario = new RepositorioFuncionarioEmArquivo(contextoDados);
        repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contextoDados);
        repositorioRequisicaoEntrada = new RepositorioRequisicaoEntradaEmArquivo(contextoDados);
    }

    [HttpGet("cadastrar/{recuperardados:bool?}")]
    public IActionResult Cadastrar(bool recuperardados)
    {
        List<Funcionario> funcionarios = repositorioFuncionario.SelecionarRegistros();
        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarRegistros();

        CadastrarRequisicaoEntradaViewModel cadastrarVM;

        if (recuperardados)
        {
            var requisicaoArmazenada = TempData.Peek("RequisicaoEntrada");

            if (requisicaoArmazenada is null && requisicaoArmazenada is not string)
            {
                NotificacaoViewModel notificacaoVM = new(
                    "Erro",
                    "requisicoes-saida",
                    "Erro ao recuperar dados anteriores. Tente novamente!");

                return View("Notificacao", notificacaoVM);
            }

            cadastrarVM = JsonSerializer.Deserialize<CadastrarRequisicaoEntradaViewModel>((requisicaoArmazenada as string)!)!;
            cadastrarVM.AdicionarFuncionarios(funcionarios);
            cadastrarVM.AdicionarMedicamentos(medicamentos);
        }
        else
        {
            TempData.Remove("RequisicaoEntrada");
            cadastrarVM = new(funcionarios, medicamentos);
        }

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarRequisicaoEntradaViewModel cadastarVM, string btnSubmit, string acaoAutomatica)
    {
        string[] submitTypes = ["selecionarFuncionario", "selecionarMedicamento", "informarQuantidade"];
        List<Funcionario> funcionarios = repositorioFuncionario.SelecionarRegistros();
        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarRegistros();

        if (TempData.TryGetValue("RequisicaoEntrada", out var value) && value is string jsonString)
        {
            var vmAnterior = JsonSerializer.Deserialize<CadastrarRequisicaoEntradaViewModel>(jsonString)!;

            vmAnterior.FuncionarioId = cadastarVM.FuncionarioId;
            vmAnterior.MedicamentoId = cadastarVM.MedicamentoId;
            vmAnterior.QuantidadeMedicamento = cadastarVM.QuantidadeMedicamento;

            cadastarVM = vmAnterior;
        }

        if (submitTypes.Contains(acaoAutomatica))
        {
            TempData["RequisicaoEntrada"] = JsonSerializer.Serialize(cadastarVM);

            return RedirectToAction("Cadastrar", new { recuperardados = true });
        }
        else if (btnSubmit == "limpar")
        {
            TempData.Remove("Prescricao");

            return RedirectToAction("Cadastrar");
        }
        else if (btnSubmit == "voltar")
        {
            TempData.Remove("Prescricao");

            return RedirectToAction("Visualizar");
        }
        else
        {
            RequisicaoEntrada novaRequisicaoEntrada = cadastarVM.ParaEntidade(funcionarios, medicamentos);

            repositorioRequisicaoEntrada.CadastrarRegistro(novaRequisicaoEntrada);
            repositorioMedicamento.AdicionarEstoque(novaRequisicaoEntrada.Medicamento!, novaRequisicaoEntrada.QuantidadeMedicamento);

            NotificacaoViewModel notificacaoVM = new(
                "Gestão de Requisição de Entrada",
                "requisicoes-entrada",
                $"Requisição feita com sucesso!\"");

            return View("Notificacao", notificacaoVM);
        }
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        List<RequisicaoEntrada> requisicoesEntrada = repositorioRequisicaoEntrada.SelecionarRegistros();

        VisualizarRequisicaoEntradaViewModel visualizarVM = new(requisicoesEntrada);

        return View(visualizarVM);
    }

    [HttpGet("excluir/{id:Guid}")]
    public IActionResult Excluir(Guid id)
    {
        RequisicaoEntrada requisicaoSelecionada = repositorioRequisicaoEntrada.SelecionarRegistroPorId(id);

        ExcluirRequisicaoEntradaViewModel excluirVM = new(
            id,
            requisicaoSelecionada.Funcionario!.Nome!,
            requisicaoSelecionada.Data);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:Guid}")]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        repositorioRequisicaoEntrada.ExcluirRegistro(id);

        NotificacaoViewModel notificacaoVM = new(
            "Gestão de Requisição de Entrada",
            "requisicoes-entrada",
            $"Requisição excluída com sucesso!\"");

        return View("Notificacao", notificacaoVM);
    }

    [HttpPost("excluirmultiplo")]
    public IActionResult ExcluirMultiplo([FromBody] List<Guid> idsSelecionados)
    {
        foreach (Guid id in idsSelecionados)
        {
            repositorioRequisicaoEntrada.ExcluirRegistro(id);
        }

        return RedirectToAction("Visualizar");
    }
}
