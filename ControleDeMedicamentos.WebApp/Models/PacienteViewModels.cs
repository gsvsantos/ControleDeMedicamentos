using ControleDeMedicamentos.WebApp.Extensions;
using ControleDeMedicamentos.WebApp.ModuloPaciente;

namespace ControleDeMedicamentos.WebApp.Models;

public abstract class FormularioPacienteViewModel
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? Telefone { get; set; }
    public string? CartaoSUS { get; set; }
}
public class CadastrarPacienteViewModel : FormularioPacienteViewModel
{
    public CadastrarPacienteViewModel() { }

    public CadastrarPacienteViewModel(string nome, string telefone, string cartaoSUS)
    {
        Nome = nome;
        Telefone = telefone;
        CartaoSUS = cartaoSUS;
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
    public EditarPacienteViewModel(Guid id, string nome, string telefone, string cartaoSUS)
    {
        Id = id;
        Nome = nome;
        Telefone = telefone;
        CartaoSUS = cartaoSUS;
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
    public DataPacienteViewModel(Guid id, string nome, string telefone, string cartaoSUS)
    {
        Id = id;
        Nome = nome;
        Telefone = telefone;
        CartaoSUS = cartaoSUS;
    }

    public override string ToString()
    {
        return $"Nome: {Nome}, Telefone: {Telefone}, CartaoSUS: {CartaoSUS}";
    }
}
