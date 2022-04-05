using Application.Dto.Movie;
using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers;

[Route("api/movie")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [SwaggerOperation(Summary = "Retrieves movies")]
    [HttpGet]
    public IActionResult Get([FromQuery]Query movieQuery)
    {
        var movies = _movieService.GetAllMovies(movieQuery);
        return Ok(movies);
    }

    [SwaggerOperation(Summary = "Retrieves a specific movie by unique id")]
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var movie = _movieService.GetMovieById(id);
        if (movie == null)
        {
            return NotFound();
        }
        return Ok(movie);
    }

    [SwaggerOperation(Summary = "Create a new movie")]
    [HttpPost]
    public IActionResult Create(CreateMovieDto newMovie)
    {
        var movie = _movieService.AddNewMovie(newMovie);
        return Created($"api/movies/{movie.Id}", movie);
    }

    [SwaggerOperation(Summary = "Update an existing movie")]
    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateMovieDto updateMovie)
    {
        _movieService.UpdateMovie(id, updateMovie);
        return NoContent();
    }

    [SwaggerOperation(Summary = "Delete an existing movie")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _movieService.DeleteMovie(id);
        return NoContent();
    }
}