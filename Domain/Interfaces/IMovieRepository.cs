using Domain.Entities;

namespace Domain.Interfaces;

public interface IMovieRepository
{
    IQueryable<Movie> GetAll();
    Movie GetById(int id);
    Movie Add(Movie movie);
    void Update(Movie movie);
    void Delete(Movie movie);
}
