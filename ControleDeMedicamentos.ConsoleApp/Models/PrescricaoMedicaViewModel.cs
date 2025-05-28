using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

namespace ControleDeMedicamentos.ConsoleApp.Models;

public class FormularioPrescricaoMedicaViewModel
{
    public Guid Id { get; set; }
    public string? CRMMedico { get; set; }
    public DateTime Data { get; set; }
    public string? Status { get; set; }
    public string? NomePaciente { get; set; }
    public Guid PacienteId { get; set; }
    public List<SelecionarPacienteViewModel> PacientesDisponiveis { get; set; } = [];
    public List<SelecionarMedicamentoViewModel> MedicamentosDisponiveis { get; set; } = [];
    public List<PrescricaoMedicamentoViewModel> MedicamentosPrescritos { get; set; } = [];
    public Guid MedicamentoId { get; set; }
    public string? DosagemMedicamento { get; set; }
    public string? PeriodoMedicamento { get; set; }
    public int QuantidadeMedicamento { get; set; }
}

public class SelecionarPacienteViewModel
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }

    public SelecionarPacienteViewModel(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class SelecionarMedicamentoViewModel
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }

    public SelecionarMedicamentoViewModel(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class CadastrarPrescricaoMedicaViewModel : FormularioPrescricaoMedicaViewModel
{
    public CadastrarPrescricaoMedicaViewModel() { }
    public CadastrarPrescricaoMedicaViewModel(List<Paciente> pacientes, List<Medicamento> medicamentos) : this()
    {
        AdicionarPacientes(pacientes);
        AdicionarMedicamentos(medicamentos);
    }

    public void AdicionarMedicamentos(List<Medicamento> medicamentos)
    {
        foreach (Medicamento m in medicamentos)
            MedicamentosDisponiveis.Add(new SelecionarMedicamentoViewModel(m.Id, m.Nome!));
    }

    public void AdicionarPacientes(List<Paciente> pacientes)
    {
        foreach (Paciente p in pacientes)
            PacientesDisponiveis.Add(new SelecionarPacienteViewModel(p.Id, p.Nome!));
    }
}

public class VisualizarPrescricaoMedicaViewModel
{
    public List<DataPrescricaoMedicaViewModel> Registros { get; set; } = [];

    public VisualizarPrescricaoMedicaViewModel(List<PrescricaoMedica> prescricoes)
    {
        foreach (PrescricaoMedica p in prescricoes)
        {
            DataPrescricaoMedicaViewModel dataVM = p.ParaDetalhesVM();

            Registros.Add(dataVM);
        }
    }
}

public class ExcluirPrescricaoMedicaViewModel : FormularioPrescricaoMedicaViewModel
{
    public ExcluirPrescricaoMedicaViewModel() { }
    public ExcluirPrescricaoMedicaViewModel(Guid id, string nomePaciente) : this()
    {
        Id = id;
        NomePaciente = nomePaciente;
    }
}

public class DataPrescricaoMedicaViewModel : FormularioPrescricaoMedicaViewModel
{
    public DataPrescricaoMedicaViewModel(
        Guid id, string cRMMedico, string nomePaciente, string status, DateTime data)
    {
        Id = id;
        CRMMedico = cRMMedico;
        NomePaciente = nomePaciente;
        Status = status;
        Data = data;
    }

    public override string ToString()
    {
        return $"CRMMedico: {CRMMedico}, \nPaciente: {NomePaciente}, \nStatus: {Status}, \nData: {Data.ToShortDateString()}";
    }
}

public class PrescricaoMedicamentoViewModel
{
    public Guid MedicamentoId { get; set; }
    public string? Medicamento { get; set; }
    public string? Dosagem { get; set; }
    public string? Periodo { get; set; }
    public int Quantidade { get; set; }

    public PrescricaoMedicamentoViewModel() { }
    public PrescricaoMedicamentoViewModel(
        Guid medicamentoId, string nomeMedicamento, string dosagem, string periodo, int quantidade
        ) : this()
    {
        MedicamentoId = medicamentoId;
        Medicamento = nomeMedicamento;
        Dosagem = dosagem;
        Periodo = periodo;
        Quantidade = quantidade;
    }
}