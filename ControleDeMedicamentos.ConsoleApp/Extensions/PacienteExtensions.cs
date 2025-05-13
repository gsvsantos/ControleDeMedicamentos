using ControleDeMedicamentos.ConsoleApp.Models;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

namespace ControleDeMedicamentos.ConsoleApp.Extensions;

public static class PacienteExtensions
{
    public static Paciente ParaEntidade(this FormularioPacienteViewModel dataVM)
    {
        return new Paciente(dataVM.Nome!, dataVM.Telefone!, dataVM.CartaoSus!);
    }
    public static DataPacienteViewModel ParaDetalhesVM(this Paciente paciente)
    {
        return new DataPacienteViewModel(paciente.Id, paciente.Nome!, paciente.Telefone!, paciente.CartaoSus!);
    }
}
