using ControleDeMedicamentos.WebApp.Compartilhado;

namespace ControleDeMedicamentos.WebApp.ModuloPrescricaoMedica;

public interface IRepositorioPrescricaoMedica : IRepositorio<PrescricaoMedica>
{
    public bool ListaVazia();
    public void VerificarValidade();
    public List<PrescricaoMedica> PegarRegistros();
}
