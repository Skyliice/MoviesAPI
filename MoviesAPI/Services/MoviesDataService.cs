﻿using AutoMapper;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;

namespace MoviesAPI.Services;

public class MoviesDataService
{
    private IRepository _context;
    private IMapper _mapper;

    public MoviesDataService(IRepository context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GenreDTO> GetGenreById(int id)
    {
        return _mapper.Map<GenreDTO>( await _context.GetGenreById(id));
    }

    public async Task AddGenre(GenreCreationDTO genre)
    {
        await _context.AddGenre(_mapper.Map<Genre>(genre));
    }

    public IQueryable<Genre> GetGenresAsQueryable()
    {
        return _context.GetGenresAsQueryable();
    }

    public async Task UpdateGenre(int id,GenreCreationDTO genreCreationDTO)
    {
        var genre = await _context.GetGenreById(id);
        genre = _mapper.Map(genreCreationDTO, genre);
        await _context.UpdateGenre(genre);
    }

    public async Task RemoveGenre(int id)
    {
        var genre = await _context.GetGenreById(id);
        await _context.DeleteGenre(genre);
    }

    public List<U> MapTo<T,U>(List<T> from)
    {
        return _mapper.Map<List<U>>(from);
    }
    
    public async Task<ActorDTO> GetActorById(int id)
    {
        return _mapper.Map<ActorDTO>( await _context.GetActorById(id));
    }

    public async Task AddActor(ActorCreationDTO actor)
    {
        await _context.AddActor(_mapper.Map<Actor>(actor));
    }

    public IQueryable<Actor> GetActorsAsQueryable()
    {
        return _context.GetActorsAsQueryable();
    }

    public async Task UpdateActor(int id,ActorCreationDTO actorCreationDTO)
    {
        var actor = await _context.GetActorById(id);
        actor = _mapper.Map(actorCreationDTO, actor);
        await _context.UpdateActor(actor);
    }

    public async Task RemoveActor(int id)
    {
        var actor = await _context.GetActorById(id);
        await _context.DeleteActor(actor);
    }
    
}