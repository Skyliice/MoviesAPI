using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Entities;
using MoviesAPI.Helpers;


namespace MoviesAPI.Services;

public class MoviesSQLServerRepository : IRepository
{
    private MoviesDbContext _context;
    
    public MoviesSQLServerRepository(MoviesDbContext context)
    {
        _context = context;
    }
    
    public async Task<Genre> GetGenreById(int id) => await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);
    public async Task AddGenre(Genre genre)
    {
        await _context.Genres.AddAsync(genre);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateGenre(Genre genre)
    {
        _context.Genres.Update(genre);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteGenre(Genre genre)
    {
        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
    }

    public IQueryable<Genre> GetGenresAsQueryable()
    {
        return  _context.Genres.AsQueryable();
    }

    public async Task<Actor> GetActorById(int id)
    {
        return await _context.Actors.FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task AddActor(Actor actor)
    {
        await _context.Actors.AddAsync(actor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateActor(Actor actor)
    {
        _context.Actors.Update(actor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteActor(Actor actor)
    {
        _context.Actors.Remove(actor);
        await _context.SaveChangesAsync();
    }

    public IQueryable<Actor> GetActorsAsQueryable()
    {
        return _context.Actors.AsQueryable();
    }
}