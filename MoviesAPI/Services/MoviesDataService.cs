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
}