using ControleDeMedicamentos.ConsoleApp.Models;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicaoSaida;

namespace ControleDeMedicamentos.ConsoleApp.Extensions;

public static class RequisicaoSaidaExtensions
{
    public static RequisicaoSaida ParaEntidade(this FormularioRequisicaoSaidaViewModel formularioVM, List<Paciente> pacientes, List<PrescricaoMedica> prescricoes)
    {
        Paciente pacienteEscolhido = pacientes.FirstOrDefault(p => p.Id == formularioVM.PacienteId)!;
        PrescricaoMedica prescricaoEscolhida = prescricoes.FirstOrDefault(pm => pm.Id == formularioVM.PrescricaoMedicaId)!;

        return new RequisicaoSaida(
            pacienteEscolhido,
            prescricaoEscolhida
        );
    }

    public static DataRequisicaoSaidaViewModel ParaDetalhesVM(this RequisicaoSaida requisicaoSaida)
    {
        return new DataRequisicaoSaidaViewModel(
            requisicaoSaida.Id,
            requisicaoSaida.Paciente!.Nome!,
            requisicaoSaida.Data
        );
    }
}