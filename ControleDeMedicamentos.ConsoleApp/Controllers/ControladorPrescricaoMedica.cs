using System.Text.Json;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.Models;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.ConsoleApp.Controllers;

[Route("prescricoes")]
public class ControladorPrescricaoMedica : Controller
{
    public ContextoDados contextoDados;
    public IRepositorioMedicamento repositorioMedicamento;
    public IRepositorioPaciente repositorioPaciente;
    public IRepositorioPrescricaoMedica repositorioPrescricaoMedica;

    public ControladorPrescricaoMedica()
    {
        contextoDados = new(true);
        repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contextoDados);
        repositorioPaciente = new RepositorioPacienteEmArquivo(contextoDados);
        repositorioPrescricaoMedica = new RepositorioPrescricaoMedicaEmArquivo(contextoDados);
        repositorioPrescricaoMedica.VerificarValidade();
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        List<Paciente> pacientes = repositorioPaciente.SelecionarRegistros();
        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarRegistros();

        CadastrarPrescricaoMedicaViewModel cadastrarVM;

        var prescricaoArmazenada = TempData.Peek("Prescricao");

        if (prescricaoArmazenada is not null && prescricaoArmazenada is string jsonString)
        {
            cadastrarVM = JsonSerializer.Deserialize<CadastrarPrescricaoMedicaViewModel>(jsonString)!;

            cadastrarVM.AdicionarMedicamentos(medicamentos);
            cadastrarVM.AdicionarPacientes(pacientes);
        }
        else
        {
            cadastrarVM = new(pacientes, medicamentos);
        }

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarPrescricaoMedicaViewModel cadastrarVM, string btnSubmit)
    {
        List<Paciente> pacientes = repositorioPaciente.SelecionarRegistros();
        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarRegistros();

        if (TempData.TryGetValue("Prescricao", out var value) && value is string jsonString)
        {
            var vmAnterior = JsonSerializer.Deserialize<CadastrarPrescricaoMedicaViewModel>(jsonString)!;

            vmAnterior.CRMMedico = cadastrarVM.CRMMedico;
            vmAnterior.PacienteId = cadastrarVM.PacienteId;
            vmAnterior.MedicamentoId = cadastrarVM.MedicamentoId;
            vmAnterior.DosagemMedicamento = cadastrarVM.DosagemMedicamento;
            vmAnterior.PeriodoMedicamento = cadastrarVM.PeriodoMedicamento;
            vmAnterior.QuantidadeMedicamento = cadastrarVM.QuantidadeMedicamento;

            cadastrarVM = vmAnterior;
        }

        if (btnSubmit == "adicionarMedicamento")
        {
            Medicamento medicamentoSelecionado = repositorioMedicamento.SelecionarRegistroPorId(cadastrarVM.MedicamentoId);

            PrescricaoMedicamentoViewModel prescricaoMedicamentoVM = new(
                cadastrarVM.MedicamentoId,
                medicamentoSelecionado.Nome!,
                cadastrarVM.DosagemMedicamento!,
                cadastrarVM.PeriodoMedicamento!,
                cadastrarVM.QuantidadeMedicamento);

            cadastrarVM.MedicamentosPrescritos.Add(prescricaoMedicamentoVM);

            TempData["Prescricao"] = JsonSerializer.Serialize(cadastrarVM);

            return RedirectToAction("Cadastrar");
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
            PrescricaoMedica novaPrescricao = cadastrarVM.ParaEntidade(pacientes, medicamentos);

            repositorioPrescricaoMedica.CadastrarRegistro(novaPrescricao);

            NotificacaoViewModel notificacaoVM = new(
                "Gestão de Prescrições Médica",
                "prescricoes",
                $"Prescrição para o paciente \"{novaPrescricao.Paciente!.Nome}\" cadastrado com sucesso!");

            TempData.Remove("Prescricao");

            return View("Notificacao", notificacaoVM);
        }
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        List<PrescricaoMedica> prescricoes = repositorioPrescricaoMedica.SelecionarRegistros();

        VisualizarPrescricaoMedicaViewModel visualizarVM = new(prescricoes);

        return View(visualizarVM);
    }

    [HttpGet("excluir/{id:Guid}")]
    public IActionResult Excluir(Guid id)
    {
        PrescricaoMedica prescricaoSelecinada = repositorioPrescricaoMedica.SelecionarRegistroPorId(id);

        ExcluirPrescricaoMedicaViewModel excluirVM = new(
            id, prescricaoSelecinada.Paciente!.Nome!);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:Guid}")]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        repositorioPrescricaoMedica.ExcluirRegistro(id);

        NotificacaoViewModel notificacaoVM = new(
            "Gestão de Prescrições Médicas",
            "prescricoes",
            $"Prescrição excluída com sucesso!");

        return View("Notificacao", notificacaoVM);
    }
}
