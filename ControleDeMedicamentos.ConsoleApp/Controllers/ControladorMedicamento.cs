using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.Models;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.ConsoleApp.Controllers;

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

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarRegistros();

        CadastrarMedicamentoViewModel cadastrarVM = new(fornecedores);

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarMedicamentoViewModel cadastrarVM)
    {
        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarRegistros();

        Medicamento novoMedicamento = cadastrarVM.ParaEntidade(fornecedores);

        repositorioMedicamento.CadastrarRegistro(novoMedicamento);

        NotificacaoViewModel notificacaoVM = new(
            "Gestão de Medicamentos",
            "medicamentos",
            $"O registro \"{novoMedicamento.Nome}\" foi cadastrado com sucesso!");

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarRegistros();

        VisualizarMedicamentosViewModel visualizarVM = new(medicamentos);

        return View(visualizarVM);
    }

    [HttpGet("editar/{id:Guid}")]
    public IActionResult Editar(Guid id)
    {
        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarRegistros();

        Medicamento medicamentoSelecionado = repositorioMedicamento.SelecionarRegistroPorId(id);

        EditarMedicamentoViewModel editarVM = new(
            id,
            medicamentoSelecionado.Nome!,
            medicamentoSelecionado.QtdEstoque!,
            medicamentoSelecionado.Descricao!,
            medicamentoSelecionado.Status!,
            fornecedores);

        return View(editarVM);
    }

    [HttpPost("editar/{id:Guid}")]
    public IActionResult Editar(Guid id, EditarMedicamentoViewModel editarVM)
    {
        List<Fornecedor> fornecedores = repositorioFornecedor.SelecionarRegistros();

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
