using ControleDeMedicamentos.WebApp.Compartilhado;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicaoEntrada;

public interface IRepositorioRequisicaoEntrada : IRepositorio<RequisicaoEntrada>
{
    public bool ListaVazia();
}