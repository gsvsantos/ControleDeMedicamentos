using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

namespace ControleDeMedicamentos.ConsoleApp.Models;

public abstract class FormularioPacienteViewModel
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? Telefone { get; set; }
    public string? CartaoSus { get; set; }
}
public class CadastrarPacienteViewModel : FormularioPacienteViewModel
{
    public CadastrarPacienteViewModel() { }

    public CadastrarPacienteViewModel(string nome, string telefone, string cartaoSus)
    {
        Nome = nome;
        Telefone = telefone;
        CartaoSus = cartaoSus;
    }
}
public class VisualizarPacienteViewModel
{
    public List<DataPacienteViewModel> Registros = [];

    public VisualizarPacienteViewModel(List<Paciente> pacientes)
    {
        foreach (Paciente p in pacientes)
        {
            DataPacienteViewModel dataVM = p.ParaDetalhesVM();

            Registros.Add(dataVM);
        }
    }
}
public class EditarPacienteViewModel : FormularioPacienteViewModel
{
    public EditarPacienteViewModel() { }
    public EditarPacienteViewModel(Guid id, string nome, string telefone, string cartaoSus)
    {
        Id = id;
        Nome = nome;
        Telefone = telefone;
        CartaoSus = cartaoSus;
    }
}
public class ExcluirPacienteViewModel : FormularioPacienteViewModel
{
    public ExcluirPacienteViewModel() { }
    public ExcluirPacienteViewModel(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}
public class DataPacienteViewModel : FormularioPacienteViewModel
{
    public DataPacienteViewModel(Guid id, string nome, string telefone, string cartaoSus)
    {
        Id = id;
        Nome = nome;
        Telefone = telefone;
        CartaoSus = cartaoSus;
    }

    public override string ToString()
    {
        return $"Nome: {Nome}, Telefone: {Telefone}, CartaoSUS: {CartaoSus}";
    }
}
