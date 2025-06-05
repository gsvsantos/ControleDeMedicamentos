using System.Text.Json;
using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Extensions;
using ControleDeMedicamentos.WebApp.Models;
using ControleDeMedicamentos.WebApp.ModuloMedicamento;
using ControleDeMedicamentos.WebApp.ModuloPaciente;
using ControleDeMedicamentos.WebApp.ModuloPrescricaoMedica;
using ControleDeMedicamentos.WebApp.ModuloRequisicaoSaida;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.Controllers;

[Route("requisicoes-saida")]
public class ControladorRequisicaoSaida : Controller
{
    private readonly ContextoDados contextoDados;
    private readonly IRepositorioMedicamento repositorioMedicamento;
    private readonly IRepositorioPaciente repositorioPaciente;
    private readonly IRepositorioPrescricaoMedica repositorioPrescricaoMedica;
    private readonly IRepositorioRequisicaoSaida repositorioRequisicaoSaida;

    public ControladorRequisicaoSaida()
    {
        contextoDados = new ContextoDados(true);
        repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contextoDados);
        repositorioPaciente = new RepositorioPacienteEmArquivo(contextoDados);
        repositorioPrescricaoMedica = new RepositorioPrescricaoMedicaEmArquivo(contextoDados);
        repositorioRequisicaoSaida = new RepositorioRequisicaoSaidaEmArquivo(contextoDados);
    }

    [HttpGet("cadastrar/{recuperardados:bool?}")]
    public IActionResult Cadastrar(bool recuperardados)
    {
        List<Paciente> pacientes = repositorioPaciente.SelecionarRegistros();
        CadastrarRequisicaoSaidaViewModel cadastrarVM;

        if (recuperardados)
        {
            var requisicaoArmazenada = TempData.Peek("RequisicaoSaida");

            if (requisicaoArmazenada is null && requisicaoArmazenada is not string)
            {
                NotificacaoViewModel notificacaoVM = new(
                    "Erro",
                    "requisicoes-saida",
                    "Erro ao recuperar dados anteriores. Tente novamente!");

                return View("Notificacao", notificacaoVM);
            }

            cadastrarVM = JsonSerializer.Deserialize<CadastrarRequisicaoSaidaViewModel>((requisicaoArmazenada as string)!)!;
            cadastrarVM.AdicionarPacientes(pacientes);
        }
        else
        {
            TempData.Remove("RequisicaoSaida");
            cadastrarVM = new(pacientes, new List<PrescricaoMedica>());
        }

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarRequisicaoSaidaViewModel cadastrarVM, string btnSubmit, string acaoAutomatica)
    {
        List<Paciente> pacientes = repositorioPaciente.SelecionarRegistros();
        List<PrescricaoMedica> prescricoes = repositorioPrescricaoMedica.SelecionarRegistros();

        if (TempData.TryGetValue("RequisicaoSaida", out var value) && value is string jsonString)
        {
            var vmAnterior = JsonSerializer.Deserialize<CadastrarRequisicaoSaidaViewModel>(jsonString)!;

            vmAnterior.PacienteId = cadastrarVM.PacienteId;
            vmAnterior.PrescricaoMedicaId = cadastrarVM.PrescricaoMedicaId;

            cadastrarVM = vmAnterior;
        }

        if (acaoAutomatica == "selecionarPaciente")
        {
            List<PrescricaoMedica> prescricoesDoPaciente = prescricoes
                .Where(pm => pm.Paciente?.Id == cadastrarVM.PacienteId && pm.Status == "Disponivel")
                .ToList();

            cadastrarVM.AdicionarPrescricoes(prescricoesDoPaciente);
            cadastrarVM.AdicionarPacientes(pacientes);

            TempData["RequisicaoSaida"] = JsonSerializer.Serialize(cadastrarVM);

            return RedirectToAction("Cadastrar", new { recuperardados = true });
        }
        else if (acaoAutomatica == "selecionarPrescricao")
        {
            TempData["RequisicaoSaida"] = JsonSerializer.Serialize(cadastrarVM);

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
            RequisicaoSaida novaRequisicaoSaida = cadastrarVM.ParaEntidade(pacientes, prescricoes);

            repositorioRequisicaoSaida.CadastrarRegistro(novaRequisicaoSaida);
            repositorioMedicamento.RemoverEstoque(novaRequisicaoSaida.PrescicaoMedica!);

            NotificacaoViewModel notificacaoVM = new(
                "Gestão de Requisição de Saída",
                "requisicoes-saida",
                $"Requisição feita com sucesso!\"");

            TempData.Remove("RequisicaoSaidaVM");

            return View("Notificacao", notificacaoVM);
        }
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        List<RequisicaoSaida> requisicoesSaida = repositorioRequisicaoSaida.SelecionarRegistros();

        VisualizarRequisicaoSaidaViewModel visualizarVM = new(requisicoesSaida);

        return View(visualizarVM);
    }

    [HttpGet("excluir/{id:Guid}")]
    public IActionResult Excluir(Guid id)
    {
        RequisicaoSaida requisicaoSelecionada = repositorioRequisicaoSaida.SelecionarRegistroPorId(id);

        ExcluirRequisicaoSaidaViewModel excluirVM = new(
            id,
            requisicaoSelecionada.Paciente!.Nome!,
            requisicaoSelecionada.Data);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:Guid}")]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        repositorioRequisicaoSaida.ExcluirRegistro(id);

        NotificacaoViewModel notificacaoVM = new(
            "Gestão de Requisição de Saída",
            "requisicoes-saida",
            $"Requisição excluída com sucesso!\"");

        return View("Notificacao", notificacaoVM);
    }
}
