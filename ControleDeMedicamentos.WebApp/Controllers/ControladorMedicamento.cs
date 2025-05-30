using System.Text.Json;
using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.Extensions;
using ControleDeMedicamentos.WebApp.Models;
using ControleDeMedicamentos.WebApp.ModuloFornecedor;
using ControleDeMedicamentos.WebApp.ModuloMedicamento;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.Controllers;

[Route("medicamentos")]
public class ControladorMedicamento : Controller
{
    private readonly ContextoDados contextoDados;
    private readonly IRepositorioFornecedor repositorioFornecedor;
    private readonly IRepositorioMedicamento repositorioMedicamento;

    public ControladorMedicamento()
    {
        contextoDados = new(true);
        repositorioFornecedor = new RepositorioFornecedorEmArquivo(contextoDados);
        repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contextoDados);
        repositorioMedicamento.VerificarEstoque();
    }

    [HttpGet("cadastrar/{recuperardados:bool?}")]
    public IActionResult Cadastrar(bool recuperardados)
    {
        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarRegistros();

        CadastrarMedicamentoViewModel cadastrarVM;

        if (recuperardados)
        {
            var dadosArmazenados = TempData.Peek("Medicamento");

            if (dadosArmazenados is null && dadosArmazenados is not string)
            {
                NotificacaoViewModel notificacaoVM = new(
                    "Erro",
                    "prescricoes",
                    "Erro ao recuperar dados anteriores. Tente novamente!"
                    );

                return View("Notificacao", notificacaoVM);
            }

            cadastrarVM = JsonSerializer.Deserialize<CadastrarMedicamentoViewModel>((dadosArmazenados as string)!)!;

            cadastrarVM.AdicionarFornecedores(fornecedores);
        }
        else
        {
            TempData.Remove("Medicamento");
            cadastrarVM = new(fornecedores);
        }

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarMedicamentoViewModel cadastrarVM, string btnSubmit, string acaoAutomatica)
    {
        string[] submitTypes = ["informarNome", "informarQuantidade", "informarDescricao", "selecionarFornecedor"];
        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarRegistros();

        if (TempData.TryGetValue("Medicamento", out var value) && value is string jsonString)
        {
            var vmAnterior = JsonSerializer.Deserialize<CadastrarMedicamentoViewModel>(jsonString);

            vmAnterior!.Nome = cadastrarVM.Nome;
            vmAnterior.QtdEstoque = cadastrarVM.QtdEstoque;
            vmAnterior.Descricao = cadastrarVM.Descricao;
            vmAnterior.FornecedorId = cadastrarVM.FornecedorId;

            cadastrarVM = vmAnterior;
        }

        if (submitTypes.Contains(acaoAutomatica))
        {
            TempData["Medicamento"] = JsonSerializer.Serialize(cadastrarVM);

            return RedirectToAction("Cadastrar", new { recuperardados = true });
        }
        else if (btnSubmit == "voltar")
        {
            TempData.Remove("Medicamento");

            return RedirectToAction("Visualizar");
        }
        else
        {
            Medicamento novoMedicamento = cadastrarVM.ParaEntidade(fornecedores);

            repositorioMedicamento.CadastrarRegistro(novoMedicamento);

            NotificacaoViewModel notificacaoVM = new(
                "Gestão de Medicamentos",
                "medicamentos",
                $"O registro \"{novoMedicamento.Nome}\" foi cadastrado com sucesso!");

            return View("Notificacao", notificacaoVM);
        }
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarRegistros();

        VisualizarMedicamentosViewModel visualizarVM = new(medicamentos);

        return View(visualizarVM);
    }

    [HttpGet("editar/{id:Guid}/{recuperardados:bool?}")]
    public IActionResult Editar(Guid id, bool recuperardados)
    {
        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarRegistros();

        EditarMedicamentoViewModel editarVM;

        if (recuperardados)
        {
            var dadosArmazenados = TempData.Peek("MedicamentoEditar");

            if (dadosArmazenados is null && dadosArmazenados is not string)
            {
                NotificacaoViewModel notificacaoVM = new(
                    "Erro",
                    "prescricoes",
                    "Erro ao recuperar dados anteriores. Tente novamente!"
                    );

                return View("Notificacao", notificacaoVM);
            }

            editarVM = JsonSerializer.Deserialize<EditarMedicamentoViewModel>((dadosArmazenados as string)!)!;

            editarVM.AdicionarFornecedores(fornecedores);
        }
        else
        {
            TempData.Remove("MedicamentoEditar");

            Medicamento medicamentoSelecionado = repositorioMedicamento.SelecionarRegistroPorId(id);

            editarVM = new(
            id,
            medicamentoSelecionado.Nome!,
            medicamentoSelecionado.QtdEstoque!,
            medicamentoSelecionado.Descricao!,
            medicamentoSelecionado.Status!,
            fornecedores);
        }

        return View(editarVM);
    }

    [HttpPost("editar/{id:Guid}")]
    public IActionResult Editar(Guid id, EditarMedicamentoViewModel editarVM, string btnSubmit, string acaoAutomatica)
    {
        string[] submitTypes = ["informarNome", "informarQuantidade", "informarDescricao", "selecionarFornecedor"];
        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarRegistros();
        if (TempData.TryGetValue("MedicamentoEditar", out var value) && value is string jsonString)
        {
            var vmAnterior = JsonSerializer.Deserialize<EditarMedicamentoViewModel>(jsonString);

            vmAnterior!.Nome = editarVM.Nome;
            vmAnterior.QtdEstoque = editarVM.QtdEstoque;
            vmAnterior.Descricao = editarVM.Descricao;
            vmAnterior.FornecedorId = editarVM.FornecedorId;

            editarVM = vmAnterior;
        }

        if (submitTypes.Contains(acaoAutomatica))
        {
            TempData["MedicamentoEditar"] = JsonSerializer.Serialize(editarVM);

            return RedirectToAction("Editar", new { recuperardados = true });
        }
        else if (btnSubmit == "cancelar")
        {
            TempData.Remove("MedicamentoEditar");

            return RedirectToAction("Visualizar");
        }
        else
        {
            Medicamento medicamentoOriginal = repositorioMedicamento.SelecionarRegistroPorId(id);
            Fornecedor fornecedorOriginal = medicamentoOriginal.Fornecedor!;

            Medicamento medicamentoEditado = editarVM.ParaEntidade(fornecedores);
            Fornecedor fornecedorEditado = medicamentoEditado.Fornecedor!;

            if (fornecedorOriginal!.Id != fornecedorEditado!.Id)
            {
                fornecedorOriginal.RemoverMedicamento(medicamentoOriginal);
                fornecedorEditado.AdicionarMedicamento(medicamentoEditado);
            }

            repositorioMedicamento.EditarRegistro(id, medicamentoEditado);

            NotificacaoViewModel notificacaoVM = new(
                "Gestão de Medicamentos",
                "medicamentos",
                $"O registro \"{medicamentoEditado.Nome}\" foi editado com sucesso!");

            return View("Notificacao", notificacaoVM);
        }
    }

    [HttpGet("excluir/{id:Guid}")]
    public IActionResult Excluir(Guid id)
    {
        Medicamento medicamentoSelecionado = repositorioMedicamento.SelecionarRegistroPorId(id);

        ExcluirMedicamentoViewModel excluirVM = new(id, medicamentoSelecionado.Nome!);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:Guid}")]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        repositorioMedicamento.ExcluirRegistro(id);

        NotificacaoViewModel notificacaoVM = new(
            "Gestão de Medicamentos",
            "medicamentos",
            $"Registro excluído com sucesso!");

        return View("Notificacao", notificacaoVM);
    }
}
