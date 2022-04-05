using Application.Dto.Movie;
using Application.Models;

namespace Application.Interfaces;

public interface IMovieService
{
    PagedResult<MovieDto> GetAllMovies(Query movieQuery);
    MovieDto GetMovieById(int id);
    MovieDto AddNewMovie(CreateMovieDto newMovie);
    void UpdateMovie(int id, UpdateMovieDto movie);
    void DeleteMovie(int id);
}
