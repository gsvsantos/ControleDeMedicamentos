using System.Diagnostics.CodeAnalysis;
using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.ModuloMedicamento;

namespace ControleDeMedicamentos.WebApp.ModuloPrescricaoMedica;

public class PrescricaoMedicamento : EntidadeBase<PrescricaoMedicamento>
{
    public string? Dosagem { get; set; }
    public string? Periodo { get; set; }
    public Medicamento? Medicamento { get; set; }
    public int Quantidade { get; set; }

    [ExcludeFromCodeCoverage]
    public PrescricaoMedicamento() { }

    public PrescricaoMedicamento(string dosagem, string periodo, Medicamento medicamento, int quantidade)
    {
        Dosagem = dosagem;
        Periodo = periodo;
        Medicamento = medicamento;
        Quantidade = quantidade;
    }

    public override void AtualizarRegistro(PrescricaoMedicamento PrescMedEditado)
    {
        Dosagem = PrescMedEditado.Dosagem;
        Periodo = PrescMedEditado.Periodo;
        Medicamento = PrescMedEditado.Medicamento;
        Quantidade = PrescMedEditado.Quantidade;
    }

    public override string Validar()
    {
        string erros = string.Empty;

        if (string.IsNullOrEmpty(Dosagem))
        {
            erros += $"O campo Dosagem eh obrigatorio.\n";
        }
        else if (Dosagem.Length < 10 || Dosagem.Length > 50)
        {
            erros += $"O campo Dosagem deve ter entre 10 e 50 caracteres.\n";
        }

        if (string.IsNullOrEmpty(Periodo))
        {
            erros += $"O campo Periodo eh obrigatorio.\n";
        }
        else if (Periodo.Length < 10 || Periodo.Length > 100)
        {
            erros += $"O campo Periodo deve ter entre 10 e 100 carateres.\n";
        }

        return erros;
    }
}
