using ControleDeMedicamentos.WebApp.Extensions;
using ControleDeMedicamentos.WebApp.ModuloFornecedor;
using ControleDeMedicamentos.WebApp.ModuloMedicamento;

namespace ControleDeMedicamentos.WebApp.Models;

public abstract class FormularioMedicamentoViewModel
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public int QtdEstoque { get; set; }
    public string? Descricao { get; set; }
    public string? Status { get; set; }
    public string? NomeFornecedor { get; set; }
    public Guid FornecedorId { get; set; }
    public List<SelecionarFornecedorViewModel> FornecedoresDisponiveis { get; set; } = [];
}

public class SelecionarFornecedorViewModel
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }

    public SelecionarFornecedorViewModel(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class CadastrarMedicamentoViewModel : FormularioMedicamentoViewModel
{
    public CadastrarMedicamentoViewModel() { }
    public CadastrarMedicamentoViewModel(List<Fornecedor> fornecedores)
    {
        AdicionarFornecedores(fornecedores);
    }

    public void AdicionarFornecedores(List<Fornecedor> fornecedores)
    {
        foreach (Fornecedor f in fornecedores)
            FornecedoresDisponiveis.Add(new SelecionarFornecedorViewModel(f.Id, f.Nome!));
    }
}

public class VisualizarMedicamentosViewModel
{
    public List<DataMedicamentoViewModel> Registros { get; set; } = [];

    public VisualizarMedicamentosViewModel(List<Medicamento> medicamentos)
    {
        foreach (Medicamento m in medicamentos)
        {
            DataMedicamentoViewModel dataVM = m.ParaDetalhesVM();

            Registros.Add(dataVM);
        }
    }
}

public class EditarMedicamentoViewModel : FormularioMedicamentoViewModel
{
    public EditarMedicamentoViewModel() { }
    public EditarMedicamentoViewModel(Guid id, string nome, int qtdEstoque, string descricao, string status, List<Fornecedor> fornecedores)
    {
        Id = id;
        Nome = nome;
        QtdEstoque = qtdEstoque;
        Descricao = descricao;
        Status = status;

        AdicionarFornecedores(fornecedores);
    }
    public void AdicionarFornecedores(List<Fornecedor> fornecedores)
    {
        foreach (Fornecedor f in fornecedores)
            FornecedoresDisponiveis.Add(new SelecionarFornecedorViewModel(f.Id, f.Nome!));
    }
}

public class ExcluirMedicamentoViewModel : FormularioMedicamentoViewModel
{
    public ExcluirMedicamentoViewModel() { }
    public ExcluirMedicamentoViewModel(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class DataMedicamentoViewModel : FormularioMedicamentoViewModel
{
    public DataMedicamentoViewModel(Guid id, string nome, int qtdEstoque, string descricao, string status, string nomeFornecedor)
    {
        Id = id;
        Nome = nome;
        QtdEstoque = qtdEstoque;
        Descricao = descricao;
        Status = status;
        NomeFornecedor = nomeFornecedor;
    }

    public override string ToString()
    {
        return $"Fornecedor; {NomeFornecedor}, Nome: {Nome},  Quantidade em Estoque: {QtdEstoque}, Descrição: {Descricao}, Status: {Status}";
    }
}
