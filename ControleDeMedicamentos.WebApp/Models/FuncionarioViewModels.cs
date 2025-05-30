using ControleDeMedicamentos.WebApp.Extensions;
using ControleDeMedicamentos.WebApp.ModuloFuncionario;

namespace ControleDeMedicamentos.WebApp.Models;

public abstract class FormularioFuncionarioViewModel
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? Telefone { get; set; }
    public string? CPF { get; set; }
}

public class CadastrarFuncionarioViewModel : FormularioFuncionarioViewModel
{
    public CadastrarFuncionarioViewModel() { }
    public CadastrarFuncionarioViewModel(string nome, string telefone, string cPF) : this()
    {
        Nome = nome;
        Telefone = telefone;
        CPF = cPF;
    }
}

public class VisualizarFuncionarioViewModel : FormularioFuncionarioViewModel
{
    public List<DataFuncionarioViewModel> Registros = [];

    public VisualizarFuncionarioViewModel(List<Funcionario> funcionarios)
    {
        foreach (Funcionario f in funcionarios)
        {
            DataFuncionarioViewModel dataVM = f.ParaDetalhesVM();

            Registros.Add(dataVM);
        }
    }
}

public class EditarFuncionarioViewModel : FormularioFuncionarioViewModel
{
    public EditarFuncionarioViewModel() { }
    public EditarFuncionarioViewModel(Guid id, string nome, string telefone, string cPF) : this()
    {
        Id = id;
        Nome = nome;
        Telefone = telefone;
        CPF = cPF;
    }
}

public class ExcluirFuncionarioViewModel : FormularioFuncionarioViewModel
{
    public ExcluirFuncionarioViewModel(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class DataFuncionarioViewModel : FormularioFuncionarioViewModel
{
    public DataFuncionarioViewModel(Guid id, string nome, string telefone, string cPF)
    {
        Id = id;
        Nome = nome;
        Telefone = telefone;
        CPF = cPF;
    }

    public override string ToString()
    {
        return $"Nome: {Nome}, Telefone: {Telefone}, CPF: {CPF}";
    }
}