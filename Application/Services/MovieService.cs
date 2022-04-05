using Application.Dto.Movie;
using Application.Exceptions;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Application.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IDiskRepository _diskRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<MovieService> _logger;

    public MovieService(IMovieRepository movieRepository, IDiskRepository diskRepository, IMapper mapper, ILogger<MovieService> logger)
    {
        _movieRepository = movieRepository;
        _diskRepository = diskRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public PagedResult<MovieDto> GetAllMovies(Query query)
    {
        var baseQuery = _movieRepository.GetAll()
            .Where(m => query.SearchPhrase == null || m.Title.ToLower().Contains(query.SearchPhrase.ToLower()));

        if(!string.IsNullOrEmpty(query.SortBy))
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Movie, object>>>
            {
                {nameof(Movie.Title), r => r.Title },
                {nameof(Movie.Detail.LastModified), r => r.Detail.LastModified },
                {nameof(Movie.Disk.Name), r => r.Disk.Name }
            };

            var selectedColumn = columnsSelector[query.SortBy];

            baseQuery = query.SortDirection == SortDirection.ASC
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var movies = baseQuery
            .Skip(query.PageSize * (query.PageNumber - 1))
            .Take(query.PageSize);

        var totalItemsCount = baseQuery.Count();

        var moviesDtos = _mapper.Map<List<MovieDto>>(movies);

        var result = new PagedResult<MovieDto>(moviesDtos, totalItemsCount, query.PageSize, query.PageNumber);

        return result;
    }

    public MovieDto GetMovieById(int id)
    {
        var movie = _movieRepository.GetById(id);

        if (movie == null)
        {
            throw new NotFoundException("This movie doesn't exist");
        }

        return _mapper.Map<MovieDto>(movie);
    }

    public MovieDto AddNewMovie(CreateMovieDto newMovie)
    {
        if (string.IsNullOrEmpty(newMovie.Title))
        {
            throw new Exception("Movie can not have an empty title");
        }

        var disk = _diskRepository.GetById(newMovie.DiskId);
        if(disk == null)
        {
            throw new NotFoundException("This disk doesn't exist");
        }

        var movie = _mapper.Map<Movie>(newMovie);

        movie.Detail = new MovieDetail
        {
            Created = DateTime.Now,
            LastModified = DateTime.Now
        };

        _movieRepository.Add(movie);
        return _mapper.Map<MovieDto>(movie);
    }

    public void UpdateMovie(int id, UpdateMovieDto movie)
    {
        if (string.IsNullOrEmpty(movie.Title))
        {
            throw new Exception("Movie can not have an empty title");
        }

        var disk = _diskRepository.GetById(movie.DiskId);
        if (disk == null)
        {
            throw new NotFoundException("This disk doesn't exist");
        }

        var existingMovie = _movieRepository.GetById(id);
        if (existingMovie == null)
        {
            throw new NotFoundException("This movie doesn't exist");
        }

        var updatedMovie = _mapper.Map(movie, existingMovie);

        updatedMovie.Detail.LastModified = DateTime.Now;

        _movieRepository.Update(updatedMovie);
    }

    public void DeleteMovie(int id)
    {
        _logger.LogError($"Movie with id: {id} DELETE action invoked");

        var movie = _movieRepository.GetById(id);
        if (movie == null)
        {
            throw new NotFoundException("This movie doesn't exist");
        }

        _movieRepository.Delete(movie);
    }
}
