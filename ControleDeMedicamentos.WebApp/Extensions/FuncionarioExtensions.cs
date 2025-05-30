using ControleDeMedicamentos.WebApp.Models;
using ControleDeMedicamentos.WebApp.ModuloFuncionario;

namespace ControleDeMedicamentos.WebApp.Extensions;

public static class FuncionarioExtensions
{
    public static Funcionario ParaEntidade(this FormularioFuncionarioViewModel formularioVM)
    {
        return new(formularioVM.Nome!, formularioVM.Telefone!, formularioVM.CPF!);
    }

    public static DataFuncionarioViewModel ParaDetalhesVM(this Funcionario funcionario)
    {
        return new(funcionario.Id, funcionario.Nome!, funcionario.Telefone!, funcionario.CPF!);
    }
}
