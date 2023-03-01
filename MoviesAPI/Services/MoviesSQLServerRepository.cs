using Microsoft.EntityFrameworkCore;
using MoviesAPI.Entities;

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
}