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
    
    public async Task<MovieTheaterDTO> GetMovieTheaterById(int id)
    {
        return _mapper.Map<MovieTheaterDTO>( await _context.GetMovieTheaterById(id));
    }

    public async Task AddMovieTheater(MovieTheaterCreationDTO movieTheaterCreationDto)
    {
        await _context.AddMovieTheater(_mapper.Map<MovieTheater>(movieTheaterCreationDto));
    }

    public IQueryable<MovieTheater> GetMovieTheatersAsQueryable()
    {
        return _context.GetMovieTheatersAsQueryable();
    }

    public async Task UpdateMovieTheater(int id,MovieTheaterCreationDTO movieTheaterCreationDto)
    {
        var movieTheater = await _context.GetMovieTheaterById(id);
        movieTheater = _mapper.Map(movieTheaterCreationDto, movieTheater);
        await _context.UpdateMovieTheater(movieTheater);
    }

    public async Task RemoveMovieTheater(int id)
    {
        var movieTheater = await _context.GetMovieTheaterById(id);
        await _context.DeleteMovieTheater(movieTheater);
    }
    
    
    public async Task<int> AddMovie(MovieCreationDTO movieCreationDto)
    {
        var movie = _mapper.Map<Movie>(movieCreationDto);
        if (movieCreationDto.Poster != null)
        {
            movie.Poster = await _fileStorageService.SaveFile("movies", movieCreationDto.Poster);
        }

        var movieGenres = await ConvertMovieGenres(movieCreationDto);
        var theaters = await ConvertMovieTheaters(movieCreationDto);
        movie.MovieTheatersMovies = theaters;
        movie.MoviesGenres = movieGenres;
        AnnotateActorsOrder(movie);
        var id = await _context.AddMovie(movie);
        return id;
    }
    
    private async Task<List<MovieTheater>> ConvertMovieTheaters(MovieCreationDTO movieCreationDto)
    {
        var theaters = _context.GetMovieTheatersAsQueryable().Where(o => movieCreationDto.MovieTheatersIds.Any(x => x == o.Id)).ToList();
        return theaters;
    }

    private async Task<List<Genre>> ConvertMovieGenres(MovieCreationDTO movieCreationDto)
    {
        var genres = _context.GetGenresAsQueryable().Where(o => movieCreationDto.GenresIds.Any(x => x == o.Id)).ToList();
        return genres;
    }

    public async Task<MovieDTO> GetMovieById(int id)
    {
        var movie = await _context.GetMovieById(id);
        if (movie == null)
        {
            return null;
        }

        var dto = _mapper.Map<MovieDTO>(movie);
        dto.Actors = dto.Actors.OrderBy(o => o.Order).ToList();
        return dto;
    }

    private void AnnotateActorsOrder(Movie movie)
    {
        if (movie.MovieActors != null)
        {
            for (int i = 0; i < movie.MovieActors.Count; i++)
            {
                movie.MovieActors[i].Order = i;
            }
        }
    }

    
}