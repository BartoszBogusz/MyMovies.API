namespace Domain.Entities;

public class MovieDetail
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }

    public int MovieId { get; set; }
    public Movie Movie { get; set; }
}
