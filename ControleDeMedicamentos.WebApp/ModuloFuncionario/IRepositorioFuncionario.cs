using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.ModuloRequisicaoEntrada;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionario;

public interface IRepositorioFuncionario : IRepositorio<Funcionario>
{
    public bool ListaVazia();

    public bool VerificarCPFInserirRegistro(Funcionario funcionario);

    public bool VerificarCPFEditarRegistro(Funcionario funcionario, Funcionario dadosEditados);

    public bool VerificarRequisicoesFuncionario(Funcionario registroEscolhido, IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada);

}