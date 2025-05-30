using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.ModuloPrescricaoMedica;
using ControleDeMedicamentos.WebApp.ModuloRequisicaoEntrada;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamento;

public interface IRepositorioMedicamento : IRepositorio<Medicamento>
{
    public bool ListaVazia();
    public void VerificarEstoque();

    public bool VerificarMedicamentoNoEstoque(Medicamento medicamento);

    public bool VerificarRequisicoesMedicamento(Medicamento medicamento, IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada);

    public void AdicionarEstoque(Medicamento medicamento, int qtdEstoque);

    public void RemoverEstoque(PrescricaoMedica prescricaoMedica);


}
