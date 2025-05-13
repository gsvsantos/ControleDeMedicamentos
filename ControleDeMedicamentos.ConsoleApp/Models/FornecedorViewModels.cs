using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;

namespace ControleDeMedicamentos.ConsoleApp.Models;

public abstract class FormularioFornecedorViewModel
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Telefone { get; set; }
    public string? CNPJ { get; set; }
}

public class CadastrarFornecedorViewModel : FormularioFornecedorViewModel
{
    public CadastrarFornecedorViewModel() { }
    public CadastrarFornecedorViewModel(string nome, string telefone, string cNPJ) : this()
    {
        Nome = nome;
        Telefone = telefone;
        CNPJ = cNPJ;
    }
}

public class VisualizarFornecedorViewModel
{
    public List<DataFornecedorViewModel> Registros { get; } = [];

    public VisualizarFornecedorViewModel(List<Fornecedor> fornecedores)
    {
        foreach (Fornecedor f in fornecedores)
        {
            DataFornecedorViewModel dataVM = f.ParaDetalhesVM();

            Registros.Add(dataVM);
        }
    }
}

public class EditarFornecedorViewModel : FormularioFornecedorViewModel
{
    public EditarFornecedorViewModel() { }
    public EditarFornecedorViewModel(int id, string nome, string telefone, string cNPJ) : this()
    {
        Id = id;
        Nome = nome;
        Telefone = telefone;
        CNPJ = cNPJ;
    }
}

public class ExcluirFornecedorViewModel : FormularioFornecedorViewModel
{
    public ExcluirFornecedorViewModel(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class DataFornecedorViewModel : FormularioFornecedorViewModel
{

    public DataFornecedorViewModel(int id, string nome, string telefone, string cNPJ)
    {
        Id = id;
        Nome = nome;
        Telefone = telefone;
        CNPJ = cNPJ;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Nome: {Nome}, Telefone: {Telefone}, CPNJ: {CNPJ}";
    }
}
