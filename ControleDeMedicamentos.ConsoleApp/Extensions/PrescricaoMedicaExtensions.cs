using ControleDeMedicamentos.ConsoleApp.Models;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricaoMedica;

namespace ControleDeMedicamentos.ConsoleApp.Extensions;

public static class PrescricaoMedicaExtensions
{
    public static PrescricaoMedica ParaEntidade(this FormularioPrescricaoMedicaViewModel formularioVM, List<Paciente> pacientes, List<Medicamento> medicamentos)
    {
        Paciente? pacienteEscolhido = pacientes.FirstOrDefault(p => p.Id == formularioVM.PacienteId)!;

        List<PrescricaoMedicamento> medicamentosPrescritos = [];

        foreach (PrescricaoMedicamentoViewModel pMViewModel in formularioVM.MedicamentosPrescritos)
        {
            Medicamento medicamentoSelecionado = medicamentos.FirstOrDefault(
                m => m.Id == pMViewModel.MedicamentoId)!;

            if (medicamentoSelecionado == null)
                continue;

            PrescricaoMedicamento prescricaoMedicamento = new(
                pMViewModel.Dosagem!,
                pMViewModel.Periodo!,
                medicamentoSelecionado,
                pMViewModel.Quantidade);

            medicamentosPrescritos.Add(prescricaoMedicamento);
        }

        return new(
            formularioVM.CRMMedico!,
            pacienteEscolhido,
            medicamentosPrescritos);
    }

    public static DataPrescricaoMedicaViewModel ParaDetalhesVM(this PrescricaoMedica prescricao)
    {
        return new(
            prescricao.Id,
            prescricao.CRMMedico!,
            prescricao.Paciente!.Nome!,
            prescricao.Status!,
            prescricao.Data);
    }
}