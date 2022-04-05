using Application.Dto.Disk;

namespace Application.Dto.Movie;

public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsWatched { get; set; }

    public DiskDto Disk { get; set; }
    public DateTime LastModified { get; set; }
}
