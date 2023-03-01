﻿using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Entities;
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
    public async Task<ActionResult<List<Genre>>> GetAllGenres()
    {
        return await _service.GetAllGenres();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Genre>> Get(int id)
    {
        var genre = await _service.GetGenreById(id);
        if (genre == null)
        {
            return NotFound();
        }
        return genre;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Genre genre)
    {
        await _service.AddGenre(genre);
        return NoContent();
    }

    [HttpPut]
    public void Put()
    {
        
    }

    [HttpDelete]
    public void Delete()
    {
        
    }
}