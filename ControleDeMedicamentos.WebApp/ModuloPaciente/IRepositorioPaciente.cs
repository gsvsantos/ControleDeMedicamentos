using ControleDeMedicamentos.WebApp.Compartilhado;

namespace ControleDeMedicamentos.WebApp.ModuloPaciente;

public interface IRepositorioPaciente : IRepositorio<Paciente>
{
    public bool ListaVazia();

    bool VerificarCartaoSUSInserirRegistro(Paciente paciente);
    bool VerificarCartaoSUSEditarRegistro(Paciente pacienteExistente, Paciente pacienteEditado);
}
