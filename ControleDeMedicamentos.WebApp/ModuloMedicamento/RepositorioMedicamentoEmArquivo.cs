using ControleDeMedicamentos.WebApp.Compartilhado;
using ControleDeMedicamentos.WebApp.ModuloPrescricaoMedica;
using ControleDeMedicamentos.WebApp.ModuloRequisicaoEntrada;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamento;

public class RepositorioMedicamentoEmArquivo : RepositorioBaseEmArquivo<Medicamento>, IRepositorioMedicamento
{
    public RepositorioMedicamentoEmArquivo(ContextoDados contexto) : base(contexto) { }

    public bool ListaVazia()
    {
        if (contexto.Medicamentos.Count <= 0)
            return true;
        else
            return false;
    }

    public override void CadastrarRegistro(Medicamento novoRegistro)
    {
        novoRegistro.Fornecedor!.AdicionarMedicamento(novoRegistro);

        base.CadastrarRegistro(novoRegistro);
    }

    public void VerificarEstoque()
    {
        foreach (Medicamento m in registros)
        {
            if (m == null)
                continue;

            if (m.QtdEstoque >= 20)
                m.Status = "Disponível";

            else
                m.Status = "Em Falta";
        }

        contexto.Salvar();
    }

    public bool VerificarMedicamentoNoEstoque(Medicamento novoRegistro)
    {
        foreach (Medicamento m in registros)
        {
            if (m == null)
                continue;

            if (novoRegistro.Nome == m.Nome && novoRegistro.Fornecedor == m.Fornecedor)
            {
                m.QtdEstoque += novoRegistro.QtdEstoque;
                novoRegistro.Fornecedor!.AdicionarMedicamento(novoRegistro);
                return true;
            }
        }

        return false;
    }

    public bool VerificarRequisicoesMedicamento(Medicamento registroEscolhido, IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada)
    {
        List<RequisicaoEntrada> requisicoesEntradas = repositorioRequisicaoEntrada.SelecionarRegistros();

        if (requisicoesEntradas.Any(rE => rE != null && rE.Medicamento!.Id == registroEscolhido.Id))
            return true;

        return false;
    }

    public void AdicionarEstoque(Medicamento registroEscolhido, int qtdEstoque)
    {
        foreach (Medicamento m in registros)
        {
            if (m == null)
                continue;

            if (m.Id == registroEscolhido.Id)
            {
                m.QtdEstoque += qtdEstoque;

                contexto.Salvar();
                return;
            }
        }
    }

    public void RemoverEstoque(PrescricaoMedica registroEscolhido)
    {
        foreach (Medicamento m in registros)
        {
            if (m == null)
                continue;

            foreach (PrescricaoMedicamento pm in registroEscolhido.Medicamentos)
            {
                if (pm == null)
                    continue;

                if (m.Id == pm.Medicamento!.Id)
                {
                    m.QtdEstoque -= pm.Quantidade;
                }
            }
        }

        contexto.Salvar();
    }

    protected override List<Medicamento> ObterRegistros()
    {
        return contexto.Medicamentos;
    }
}
