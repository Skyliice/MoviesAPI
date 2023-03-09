using MoviesAPI.Entities;

namespace MoviesAPI;

public interface IRepository
{
    public Task<Genre> GetGenreById(int id);
    public Task AddGenre(Genre genre);
    public Task UpdateGenre(Genre genre);
    public Task DeleteGenre(Genre genre);
    public IQueryable<Genre> GetGenresAsQueryable();
    
    public Task<Actor> GetActorById(int id);
    public Task AddActor(Actor actor);
    public Task UpdateActor(Actor actor);
    public Task DeleteActor(Actor actor);
    public IQueryable<Actor> GetActorsAsQueryable();
}