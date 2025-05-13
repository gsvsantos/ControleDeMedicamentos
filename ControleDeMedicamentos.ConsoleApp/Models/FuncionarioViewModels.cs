using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionario;

namespace ControleDeMedicamentos.ConsoleApp.Models;

public abstract class FormularioFuncionarioViewModel
{
    public int Id { get; set; }
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
    public EditarFuncionarioViewModel(int id, string nome, string telefone, string cPF) : this()
    {
        Id = id;
        Nome = nome;
        Telefone = telefone;
        CPF = cPF;
    }
}

public class ExcluirFuncionarioViewModel : FormularioFuncionarioViewModel
{
    public ExcluirFuncionarioViewModel(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class DataFuncionarioViewModel : FormularioFuncionarioViewModel
{
    public DataFuncionarioViewModel(int id, string nome, string telefone, string cPF)
    {
        Id = id;
        Nome = nome;
        Telefone = telefone;
        CPF = cPF;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Nome: {Nome}, Telefone: {Telefone}, CPF: {CPF}";
    }
}