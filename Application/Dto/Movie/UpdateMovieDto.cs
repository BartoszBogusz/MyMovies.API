namespace Application.Dto.Movie;

public class UpdateMovieDto
{
    public string Title { get; set; }
    public int DiskId { get; set; }
    public bool IsWatched { get; set; }
}
