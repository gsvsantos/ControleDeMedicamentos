using ControleDeMedicamentos.ConsoleApp.Models;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

namespace ControleDeMedicamentos.ConsoleApp.Extensions;

public static class MedicamentoExtensions
{
    public static Medicamento ParaEntidade(this FormularioMedicamentoViewModel formularioVM, List<Fornecedor> fornecedores)
    {
        Fornecedor fornecedorSelecionado = null!;

        foreach (Fornecedor f in fornecedores)
        {
            if (f.Id == formularioVM.FornecedorId)
                fornecedorSelecionado = f;
        }

        return new(
            formularioVM.Nome!,
            formularioVM.QtdEstoque,
            formularioVM.Descricao!,
            fornecedorSelecionado);
    }

    public static DataMedicamentoViewModel ParaDetalhesVM(this Medicamento medicamento)
    {
        return new(
            medicamento.Id,
            medicamento.Nome!,
            medicamento.QtdEstoque,
            medicamento.Descricao!,
            medicamento.Status!,
            medicamento.Fornecedor!.Nome!);
    }
}
