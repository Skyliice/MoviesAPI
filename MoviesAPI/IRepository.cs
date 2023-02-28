using MoviesAPI.Entities;

namespace MoviesAPI;

public interface IRepository
{
    public List<Genre> GetAllGenres();
}