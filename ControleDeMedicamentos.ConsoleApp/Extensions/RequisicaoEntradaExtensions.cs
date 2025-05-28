using ControleDeMedicamentos.ConsoleApp.Models;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoEntrada;

namespace ControleDeMedicamentos.ConsoleApp.Extensions;

public static class RequisicaoEntradaExtensions
{
    public static RequisicaoEntrada ParaEntidade(this FormularioRequisicaoEntradaViewModel formularioVM, List<Funcionario> funcionarios, List<Medicamento> medicamentos)
    {
        Funcionario funcionarioEscolhido = funcionarios.FirstOrDefault(f => f.Id == formularioVM.FuncionarioId)!;
        Medicamento medicamentoEscolhido = medicamentos.FirstOrDefault(m => m.Id == formularioVM.MedicamentoId)!;

        return new(
            medicamentoEscolhido,
            funcionarioEscolhido,
            formularioVM.QuantidadeMedicamento);
    }

    public static DataRequisicaoEntradaViewModel ParaDetalhesVM(this RequisicaoEntrada requisicaoEntrada)
    {
        return new(
            requisicaoEntrada.Id,
            requisicaoEntrada.Funcionario!.Nome!,
            requisicaoEntrada.Medicamento!.Nome!,
            requisicaoEntrada.QuantidadeMedicamento,
            requisicaoEntrada.Data);
    }
}
