using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using MoviesAPI.Helpers;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers;

[Route("api/movies")]
[ApiController]
public class MoviesController : Controller
{
    private MoviesDataService _service;

    public MoviesController(MoviesDataService service)
    {
        _service = service;
    }

    [HttpGet("PostGet")]
    public async Task<ActionResult<MoviePostGetDTO>> PostGet()
    {
        var movieTheaters = await _service.GetMovieTheatersAsQueryable().ToListAsync();
        var genres = await _service.GetGenresAsQueryable().ToListAsync();

        var movieTheatersDTO = _service.MapTo<MovieTheater,MovieTheaterDTO>(movieTheaters);
        var genresDTO = _service.MapTo<Genre, GenreDTO>(genres);

        return new MoviePostGetDTO() { Genres = genresDTO, MovieTheaters = movieTheatersDTO };
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MovieDTO>> Get(int id)
    {
        var movie = await _service.GetMovieById(id);
        return movie;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post([FromForm] MovieCreationDTO movieCreationDto)
    {
        var id = await _service.AddMovie(movieCreationDto);
        return id;
    }
}