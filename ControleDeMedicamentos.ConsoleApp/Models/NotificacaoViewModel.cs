namespace ControleDeMedicamentos.ConsoleApp.Models;

public class NotificacaoViewModel
{
    public string? Titulo { get; set; }
    public string? Caminho { get; set; }
    public string? Mensagem { get; set; }

    public NotificacaoViewModel(string? titulo, string? caminho, string? mensagem)
    {
        Titulo = titulo;
        Caminho = caminho;
        Mensagem = mensagem;
    }
}
