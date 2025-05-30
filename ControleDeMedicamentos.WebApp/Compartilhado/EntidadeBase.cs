namespace ControleDeMedicamentos.WebApp.Compartilhado;

public abstract class EntidadeBase<Tipo>
{
    public Guid Id { get; set; }

    public abstract void AtualizarRegistro(Tipo registroEditado);
    public abstract string Validar();
}
