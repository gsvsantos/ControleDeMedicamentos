public class ExcluirMultiploViewModel
{
    public List<Guid> Ids { get; set; }

    public ExcluirMultiploViewModel(List<Guid> ids)
    {
        Ids = ids;
    }
}