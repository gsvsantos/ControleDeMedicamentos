namespace ControleDeMedicamentos.ConsoleApp.Compartilhado;

public static class ViewBagHelper
{
    public static void DefinirDados(dynamic viewBag, string contexto, string tipo, string acao, string nomeRegistro = "")
    {
        viewBag.Contexto = contexto;
        viewBag.Tipo = tipo;

        if (acao != "excluído")
            viewBag.Mensagem = $"O registro \"{nomeRegistro}\" foi {acao} com sucesso!";
        else
            viewBag.Mensagem = "Registro excluído com sucesso!";
    }
}
