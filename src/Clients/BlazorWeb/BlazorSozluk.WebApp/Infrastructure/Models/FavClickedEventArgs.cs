namespace BlazorSozluk.WebApp.Infrastructure.Models;

public class FavClickedEventArgs
{
    public Guid? EntryId { get; set; }

    public bool IsFaved { get; set; }
}
