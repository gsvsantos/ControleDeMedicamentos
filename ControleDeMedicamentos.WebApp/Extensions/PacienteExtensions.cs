using ControleDeMedicamentos.WebApp.Models;
using ControleDeMedicamentos.WebApp.ModuloPaciente;

namespace ControleDeMedicamentos.WebApp.Extensions;

public static class PacienteExtensions
{
    public static Paciente ParaEntidade(this FormularioPacienteViewModel dataVM)
    {
        return new Paciente(dataVM.Nome!, dataVM.Telefone!, dataVM.CartaoSUS!);
    }
    public static DataPacienteViewModel ParaDetalhesVM(this Paciente paciente)
    {
        return new DataPacienteViewModel(paciente.Id, paciente.Nome!, paciente.Telefone!, paciente.CartaoSUS!);
    }
}
