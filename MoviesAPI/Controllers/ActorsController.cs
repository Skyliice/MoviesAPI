﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using MoviesAPI.Helpers;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers;

[Route("api/actors")]
[ApiController]
public class ActorsController : Controller
{
    private MoviesDataService _service;

    public ActorsController(MoviesDataService service)
    {
        _service = service;
    }
    // GET
    [HttpGet]
    [ActionName("get")]
    public async Task<ActionResult<List<ActorDTO>>> GetActors([FromQuery] PaginationDTO paginationDTO)
    {
        var queryable = _service.GetActorsAsQueryable();
        await HttpContext.InsertParametersPaginationInHeader(queryable);
        var actors = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
        return _service.MapTo<Actor,ActorDTO>(actors);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ActorDTO>> Get(int id)
    {
        var actor = await _service.GetActorById(id);
        if (actor == null)
        {
            return NotFound();
        }
        return actor;
    }

    [HttpGet("searchByName/{query}")]
    public async Task<ActionResult<List<ActorsMovieDTO>>> SearchByName(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return new List<ActorsMovieDTO>();
        return await _service.GetActorsAsQueryable()
            .Where(o => o.Name.Contains(query))
            .OrderBy(o=>o.Name)
            .Select(o=> new ActorsMovieDTO(){Id = o.Id, Name = o.Name, Picture = o.Picture})
            .Take(5)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromForm] ActorCreationDTO actor)
    {
        await _service.AddActor(actor);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id,[FromForm] ActorCreationDTO actor)
    {
        await _service.UpdateActor(id,actor);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _service.RemoveActor(id);
        return NoContent();
    }
}