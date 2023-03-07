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

    public async Task<List<Genre>> GetAllGenres()
    {
        return await _context.Genres.ToListAsync();
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
}