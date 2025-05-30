using ControleDeMedicamentos.WebApp.Models;
using ControleDeMedicamentos.WebApp.ModuloPaciente;
using ControleDeMedicamentos.WebApp.ModuloPrescricaoMedica;
using ControleDeMedicamentos.WebApp.ModuloRequisicaoSaida;

namespace ControleDeMedicamentos.WebApp.Extensions;

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