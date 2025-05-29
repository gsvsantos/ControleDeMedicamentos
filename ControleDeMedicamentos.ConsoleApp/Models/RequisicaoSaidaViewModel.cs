using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoSaida;

namespace ControleDeMedicamentos.ConsoleApp.Models;

public class FormularioRequisicaoSaidaViewModel
{
    public Guid Id { get; set; }
    public string? IdShorted { get; set; }
    public DateTime Data { get; set; }
    public Guid PacienteId { get; set; }
    public string? NomePaciente { get; set; }
    public List<SelecionarPacienteViewModel> PacientesDisponiveis { get; set; } = [];
    public Guid PrescricaoMedicaId { get; set; }
    public List<SelecionarPrescricaoMedicaViewModel> PrescricoesDisponiveis { get; set; } = [];
    public List<PrescricaoMedicamentoSaidaViewModel> MedicamentosPrescritos { get; set; } = [];
}

public class SelecionarPrescricaoMedicaViewModel
{
    public Guid Id { get; set; }
    public string? Descricao { get; set; }

    public SelecionarPrescricaoMedicaViewModel(Guid id, string descricao)
    {
        Id = id;
        Descricao = descricao;
    }
}

public class CadastrarRequisicaoSaidaViewModel : FormularioRequisicaoSaidaViewModel
{
    public CadastrarRequisicaoSaidaViewModel() { }
    public CadastrarRequisicaoSaidaViewModel(List<Paciente> pacientes, List<PrescricaoMedica> prescricoes) : this()
    {
        AdicionarPacientes(pacientes);
        AdicionarPrescricoes(prescricoes);
    }
    public void AdicionarPacientes(List<Paciente> pacientes)
    {
        PacientesDisponiveis.Clear();

        foreach (Paciente p in pacientes)
            PacientesDisponiveis.Add(new SelecionarPacienteViewModel(p.Id, p.Nome!));
    }
    public void AdicionarPrescricoes(List<PrescricaoMedica> prescricoes)
    {
        PrescricoesDisponiveis.Clear();

        foreach (PrescricaoMedica pM in prescricoes)
        {
            string idShorted = pM.Id.ToString()[..5];
            string descricao = $"{pM.Data:dd/MM/yyyy HH: mm} - {pM.Paciente!.Nome} - ID: {idShorted}";

            PrescricoesDisponiveis.Add(new SelecionarPrescricaoMedicaViewModel(pM.Id, descricao));
        }
    }
}

public class VisualizarRequisicaoSaidaViewModel
{
    public List<DataRequisicaoSaidaViewModel> Registros { get; set; } = [];

    public VisualizarRequisicaoSaidaViewModel(List<RequisicaoSaida> requisicoesSaida)
    {
        foreach (RequisicaoSaida rS in requisicoesSaida)
        {
            DataRequisicaoSaidaViewModel dataVM = rS.ParaDetalhesVM();

            Registros.Add(dataVM);
        }
    }
}

public class ExcluirRequisicaoSaidaViewModel : FormularioRequisicaoSaidaViewModel
{
    public ExcluirRequisicaoSaidaViewModel(Guid id, string nomePaciente, DateTime data)
    {
        Id = id;
        NomePaciente = nomePaciente;
        Data = data;
        IdShorted = id.ToString()[..5];
    }
}

public class DataRequisicaoSaidaViewModel : FormularioRequisicaoSaidaViewModel
{
    public DataRequisicaoSaidaViewModel(Guid id, string nomePaciente, DateTime data)
    {
        Id = id;
        NomePaciente = nomePaciente;
        Data = data;
    }
}

public class PrescricaoMedicamentoSaidaViewModel
{
    public Guid MedicamentoId { get; set; }
    public string? Medicamento { get; set; }
    public string? Dosagem { get; set; }
    public string? Periodo { get; set; }
    public int Quantidade { get; set; }

    public PrescricaoMedicamentoSaidaViewModel() { }
    public PrescricaoMedicamentoSaidaViewModel(
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
