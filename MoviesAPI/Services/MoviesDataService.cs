using AutoMapper;
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

    public async Task<List<GenreDTO>> GetAllGenres()
    {
        return _mapper.Map<List<GenreDTO>>(await _context.GetAllGenres());
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

    public List<GenreDTO> MaptoGenreDTO(List<Genre> genres)
    {
        return _mapper.Map<List<GenreDTO>>(genres);
    }
    
}