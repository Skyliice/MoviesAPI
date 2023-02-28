using MoviesAPI.Entities;

namespace MoviesAPI;

public interface IRepository
{
    public Task<List<Genre>> GetAllGenres();
    public Genre GetGenreById(int id);
    public void AddGenre(Genre genre);
}