using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using MoviesAPI.Helpers;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers;

[Route("/api/movietheaters")]
[ApiController]
public class MovieTheatersController : Controller
{
    private MoviesDataService _service;

    public MovieTheatersController(MoviesDataService service)
    {
        _service = service;
    }
    // GET
    [HttpGet]
    public async Task<ActionResult<List<MovieTheaterDTO>>> GetMovieTheaters([FromQuery] PaginationDTO paginationDTO)
    {
        var queryable = _service.GetMovieTheatersAsQueryable();
        await HttpContext.InsertParametersPaginationInHeader(queryable);
        var movieTheaters = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
        return _service.MapTo<MovieTheater,MovieTheaterDTO>(movieTheaters);
    }
    
    [HttpGet("${int:id}")]
    public async Task<ActionResult<MovieTheaterDTO>> GetMovieTheaterById(int id)
    {
        var movieTheater = await _service.GetMovieTheaterById(id);
        if (movieTheater == null)
        {
            return NotFound();
        }
        return movieTheater;
    }
    
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] MovieTheaterCreationDTO movieTheaterCreationDto)
    {
        await _service.AddMovieTheater(movieTheaterCreationDto);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id,[FromBody] MovieTheaterCreationDTO movieTheaterCreationDto)
    {
        await _service.UpdateMovieTheater(id,movieTheaterCreationDto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _service.RemoveMovieTheater(id);
        return NoContent();
    }
}