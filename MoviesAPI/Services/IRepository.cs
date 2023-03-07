using MoviesAPI.Entities;

namespace MoviesAPI;

public interface IRepository
{
    public Task<List<Genre>> GetAllGenres();
    public Task<Genre> GetGenreById(int id);
    public Task AddGenre(Genre genre);
    public Task UpdateGenre(Genre genre);
    public Task DeleteGenre(Genre genre);
    public IQueryable<Genre> GetGenresAsQueryable();
}