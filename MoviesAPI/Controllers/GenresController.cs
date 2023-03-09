using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using MoviesAPI.Helpers;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GenresController : Controller
{
    private readonly MoviesDataService _service;
    public GenresController(MoviesDataService service)
    {
        _service = service;
    }
    // GET
    [HttpGet]
    public async Task<ActionResult<List<GenreDTO>>> GetAllGenres([FromQuery] PaginationDTO paginationDTO)
    {
        var queryable = _service.GetGenresAsQueryable();
        await HttpContext.InsertParametersPaginationInHeader(queryable);
        var genres = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
        return _service.MapTo<Genre,GenreDTO>(genres);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GenreDTO>> Get(int id)
    {
        var genre = await _service.GetGenreById(id);
        if (genre == null)
        {
            return NotFound();
        }
        return genre;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] GenreCreationDTO genre)
    {
        await _service.AddGenre(genre);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id,[FromBody] GenreCreationDTO genre)
    {
        await _service.UpdateGenre(id,genre);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _service.RemoveGenre(id);
        return NoContent();
    }
}