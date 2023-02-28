using MoviesAPI.Entities;

namespace MoviesAPI;

public interface IRepository
{
    public List<Genre> GetAllGenres();
    public Genre GetGenreById(int id);
}