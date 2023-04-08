using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using NetTopologySuite.Geometries;

namespace MoviesAPI.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles(GeometryFactory geometryFactory)
    {
        CreateMap<GenreDTO, Genre>().ReverseMap();
        CreateMap<GenreCreationDTO, Genre>();
        
        CreateMap<ActorDTO, Actor>().ReverseMap();
        CreateMap<ActorCreationDTO, Actor>()
            .ForMember(x => x.Picture, options => options.Ignore());

        CreateMap<MovieTheater, MovieTheaterDTO>()
            .ForMember(x => x.Latitude, dto => dto.MapFrom(prop => prop.Location.Y))
            .ForMember(x => x.Longitude, dto => dto.MapFrom(prop => prop.Location.X));
        CreateMap<MovieTheaterCreationDTO, MovieTheater>()
            .ForMember(x=>x.Location, x => x.MapFrom(dto => 
                geometryFactory.CreatePoint(new Coordinate(dto.Longitude,dto.Latitude))));

        CreateMap<MovieCreationDTO, Movie>()
            .ForMember(x => x.Poster, options => options.Ignore())
            .ForMember(x => x.MovieActors, options => options.MapFrom(MapMoviesActors));


        CreateMap<Movie, MovieDTO>()
            .ForMember(o => o.Actors, options => options.MapFrom(MapMoviesActors))
            .ForMember(o => o.Genres, options => options.MapFrom(MapMoviesGenres))
            .ForMember(o=>o.MovieTheaters, options => options.MapFrom(MapMovieTheaters));
    }

    private List<MovieTheaterDTO> MapMovieTheaters(Movie movie, MovieDTO movieDto)
    {
        var rslt = new List<MovieTheaterDTO>();
        if (movie.MoviesGenres != null)
        {
            foreach (var movieTheater in movie.MovieTheatersMovies)
            {
                rslt.Add(new MovieTheaterDTO()
                {
                    Id = movieTheater.Id,
                    Name = movieTheater.Name,
                    Longitude = movieTheater.Location.X,
                    Latitude = movieTheater.Location.Y
                });
            }
        }
        return rslt;
    }

    private List<GenreDTO> MapMoviesGenres(Movie movie,MovieDTO movieDto)
    {
        var rslt = new List<GenreDTO>();
        if (movie.MoviesGenres != null)
        {
            foreach (var genre in movie.MoviesGenres)
            {
                rslt.Add(new GenreDTO()
                {
                    Id = genre.Id,
                    Name = genre.Name
                });
            }
        }
        return rslt;
    }

    private List<ActorsMovieDTO> MapMoviesActors(Movie movie, MovieDTO movieDTO)
    {
        var result = new List<ActorsMovieDTO>();
        if (movie.MovieActors != null)
        {
            foreach (var movieActor in movie.MovieActors)
            {
                result.Add(new ActorsMovieDTO()
                {
                    Id = movieActor.ActorId,
                    Name = movieActor.Actor.Name,
                    Character = movieActor.Character,
                    Picture = movieActor.Actor.Picture,
                    Order = movieActor.Order
                });
            }
        }
        return result;
    }

    private List<MoviesActors> MapMoviesActors(MovieCreationDTO movieCreationDto, Movie movie)
    {
        var result = new List<MoviesActors>();
        if (movieCreationDto.Actors == null)
        {
            return result;
        }

        foreach (var actor in movieCreationDto.Actors)
        {
            result.Add(new MoviesActors(){ActorId = actor.Id , Character = actor.Character});
        }

        return result;
    }
    
}