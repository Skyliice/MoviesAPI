using AutoMapper;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;

namespace MoviesAPI.Services;

public class MoviesDataService
{
    private IRepository _context;
    private IMapper _mapper;
    private IFileStorageService _fileStorageService;

    public MoviesDataService(IRepository context,IMapper mapper, IFileStorageService fileStorageService)
    {
        _context = context;
        _mapper = mapper;
        _fileStorageService = fileStorageService;
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

    public List<U> MapTo<T,U>(List<T> from)
    {
        return _mapper.Map<List<U>>(from);
    }
    
    public async Task<ActorDTO> GetActorById(int id)
    {
        return _mapper.Map<ActorDTO>( await _context.GetActorById(id));
    }

    public async Task AddActor(ActorCreationDTO actorCreationDto)
    {
        var actor = _mapper.Map<Actor>(actorCreationDto);
        if (actorCreationDto.Picture != null)
        {
            actor.Picture = await _fileStorageService.SaveFile("actors", actorCreationDto.Picture);
        }
        await _context.AddActor(actor);
    }

    public IQueryable<Actor> GetActorsAsQueryable()
    {
        return _context.GetActorsAsQueryable();
    }

    public async Task UpdateActor(int id,ActorCreationDTO actorCreationDTO)
    {
        var actor = await _context.GetActorById(id);
        if (actor == null)
        {
            return;
        }

        if (actorCreationDTO.Picture != null)
        {
            actor.Picture = await _fileStorageService.EditFile("actors",actorCreationDTO.Picture,actor.Picture);
        }
        actor = _mapper.Map(actorCreationDTO, actor);
        await _context.UpdateActor(actor);
    }

    public async Task RemoveActor(int id)
    {
        var actor = await _context.GetActorById(id);
        if (actor == null)
        {
            return;
        }
        await _context.DeleteActor(actor);
        await _fileStorageService.DeleteFile(actor.Picture, "actors");
    }
    
}