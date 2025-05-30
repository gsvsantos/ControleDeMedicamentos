using ControleDeMedicamentos.WebApp.Models;
using ControleDeMedicamentos.WebApp.ModuloFornecedor;

namespace ControleDeMedicamentos.WebApp.Extensions;

public static class FornecedorExtensions
{
    // Método de Extensão -> Permite chamar métodos novos diretamente nas classes, em any tipos
    public static Fornecedor ParaEntidade(this FormularioFornecedorViewModel formularioVM)
    {
        return new(formularioVM.Nome!, formularioVM.Telefone!, formularioVM.CNPJ!);
    }

    public static DataFornecedorViewModel ParaDetalhesVM(this Fornecedor fornecedor)
    {
        return new(fornecedor.Id, fornecedor.Nome!, fornecedor.Telefone!, fornecedor.CNPJ!);
    }
}
