using System.Diagnostics.CodeAnalysis;
using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.ModuloPaciente;
using ControleDeMedicamentos.WebApp.ModuloPrescricaoMedica;

namespace ControleDeMedicamentos.WebApp.ModuloRequisicaoSaida;

public class RequisicaoSaida : EntidadeBase<RequisicaoSaida>
{
    public DateTime Data { get; set; }
    public Paciente? Paciente { get; set; }
    public PrescricaoMedica? PrescicaoMedica { get; set; }
    public List<PrescricaoMedicamento> MedicamentosRequisitados { get; set; } = [];

    [ExcludeFromCodeCoverage]
    public RequisicaoSaida() { }

    public RequisicaoSaida(Paciente paciente, PrescricaoMedica prescicaoMedica)
    {
        Data = DateTime.Now;
        Paciente = paciente;
        PrescicaoMedica = prescicaoMedica;
    }

    public override void AtualizarRegistro(RequisicaoSaida registroEditado)
    {
        Data = registroEditado.Data;
        Paciente = registroEditado.Paciente;
        PrescicaoMedica = registroEditado.PrescicaoMedica;
        MedicamentosRequisitados = registroEditado.MedicamentosRequisitados;
    }

    public override string Validar()
    {
        string erros = "";

        if (Paciente == null)
            erros += "O paciente selecionado não está registrado.\n";

        if (PrescicaoMedica == null)
            erros += "A prescrição médica selecionada não está registrado.\n";

        if (MedicamentosRequisitados == null)
            erros += "Os medicamentos requisitados são inválidos!\n";

        return erros;
    }

    public void PegarMedicamentosRequisitados(PrescricaoMedica prescricaoMedica)
    {
        MedicamentosRequisitados = prescricaoMedica.Medicamentos;
    }
}
