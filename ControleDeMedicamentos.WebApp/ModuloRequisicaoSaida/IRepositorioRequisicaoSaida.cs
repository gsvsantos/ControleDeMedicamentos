using ControleDeMedicamentos.WebApp.Compartilhado;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicaoSaida;

public interface IRepositorioRequisicaoSaida : IRepositorio<RequisicaoSaida>
{
    public bool ListaVazia();

    public bool VerificarEstoqueExcedido(RequisicaoSaida novoRegistro);

    public bool VerificarEstoquePosRequisicao(RequisicaoSaida novoRegistro);

    public bool VerificarPacientePrescricao(RequisicaoSaida novoRegistro);
}