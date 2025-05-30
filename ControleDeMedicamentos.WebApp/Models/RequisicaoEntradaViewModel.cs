using ControleDeMedicamentos.WebApp.Extensions;
using ControleDeMedicamentos.WebApp.ModuloFuncionario;
using ControleDeMedicamentos.WebApp.ModuloMedicamento;
using ControleDeMedicamentos.WebApp.ModuloRequisicaoEntrada;

namespace ControleDeMedicamentos.WebApp.Models;

public class FormularioRequisicaoEntradaViewModel
{
    public Guid Id { get; set; }
    public string? IdShorted { get; set; }
    public DateTime Data { get; set; }
    public Guid MedicamentoId { get; set; }
    public string? NomeMedicamento { get; set; }
    public List<SelecionarMedicamentoViewModel> MedicamentosDisponiveis { get; set; } = [];
    public Guid FuncionarioId { get; set; }
    public string? NomeFuncionario { get; set; }
    public List<SelecionarFuncionarioViewModel> FuncionariosDisponiveis { get; set; } = [];
    public int QuantidadeMedicamento { get; set; }
}

public class SelecionarFuncionarioViewModel
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }

    public SelecionarFuncionarioViewModel(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class CadastrarRequisicaoEntradaViewModel : FormularioRequisicaoEntradaViewModel
{
    public CadastrarRequisicaoEntradaViewModel() { }
    public CadastrarRequisicaoEntradaViewModel(List<Funcionario> funcionarios, List<Medicamento> medicamentos) : this()
    {
        AdicionarFuncionarios(funcionarios);
        AdicionarMedicamentos(medicamentos);
    }
    public void AdicionarFuncionarios(List<Funcionario> funcionarios)
    {
        foreach (Funcionario f in funcionarios)
            FuncionariosDisponiveis.Add(new SelecionarFuncionarioViewModel(f.Id, f.Nome!));
    }
    public void AdicionarMedicamentos(List<Medicamento> medicamentos)
    {
        foreach (Medicamento m in medicamentos)
            MedicamentosDisponiveis.Add(new SelecionarMedicamentoViewModel(m.Id, m.Nome!));
    }
}

public class VisualizarRequisicaoEntradaViewModel
{
    public List<DataRequisicaoEntradaViewModel> Registros { get; set; } = [];

    public VisualizarRequisicaoEntradaViewModel(List<RequisicaoEntrada> requisicoesEntrada)
    {
        foreach (RequisicaoEntrada rE in requisicoesEntrada)
        {
            DataRequisicaoEntradaViewModel dataVM = rE.ParaDetalhesVM();

            Registros.Add(dataVM);
        }
    }
}

public class ExcluirRequisicaoEntradaViewModel : FormularioRequisicaoEntradaViewModel
{
    public ExcluirRequisicaoEntradaViewModel(Guid id, string nomeFuncionario, DateTime data)
    {
        Id = id;
        NomeFuncionario = nomeFuncionario;
        Data = data;
        IdShorted = id.ToString()[..5];
    }
}

public class DataRequisicaoEntradaViewModel : FormularioRequisicaoEntradaViewModel
{
    public DataRequisicaoEntradaViewModel(Guid id, string nomeFuncionario, string nomeMedicamento, int quantidadeMedicamento, DateTime data)
    {
        Id = id;
        NomeFuncionario = nomeFuncionario;
        NomeMedicamento = nomeMedicamento;
        QuantidadeMedicamento = quantidadeMedicamento;
        Data = data;
    }
}
