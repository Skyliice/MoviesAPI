using MoviesAPI.Entities;

namespace MoviesAPI.Services;

public class MoviesDataService
{
    private IRepository _context;

    public MoviesDataService(IRepository context)
    {
        _context = context;
    }

    public async Task<List<Genre>> GetAllGenres()
    {
        return await _context.GetAllGenres();
    }

    public async Task<Genre> GetGenreById(int id)
    {
        return await _context.GetGenreById(id);
    }

    public async Task AddGenre(Genre genre)
    {
        await _context.AddGenre(genre);
    }
}