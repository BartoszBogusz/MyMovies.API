namespace Domain.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsWatched { get; set; }
    public MovieDetail Detail { get; set; }

    public int DiskId { get; set; }
    public Disk Disk { get; set; }
}
