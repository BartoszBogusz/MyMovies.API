namespace Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    private MyMoviesContext _context;

    public MovieRepository(MyMoviesContext context)
    {
        _context = context;
    }
    public IQueryable<Movie> GetAll()
    {
        return _context.Movies
            .Include(x => x.Detail)
            .Include(x => x.Disk);
    }

    public Movie GetById(int id)
    {
        return _context.Movies
            .Include(x => x.Detail)
            .Include(x => x.Disk)
            .SingleOrDefault(x => x.Id == id);
    }

    public Movie Add(Movie movie)
    {
        _context.Movies.Add(movie);
        _context.SaveChanges();
        return movie;
    }

    public void Update(Movie movie)
    {
        _context.Movies.Update(movie);
        _context.SaveChanges();
    }

    public void Delete(Movie movie)
    {
        _context.Movies.Remove(movie);
        _context.SaveChanges();
    }
}
