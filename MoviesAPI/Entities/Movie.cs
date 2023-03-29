using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Entities;

public class Movie
{
    public int Id { get; set; }
    [StringLength(maximumLength:75)]
    [Required]
    public string Title { get; set; }
    public string Summary { get; set; }
    public string Trailer { get; set; }
    public bool InTheaters { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Poster { get; set; }
    public List<Genre> MoviesGenres { get; set; }
    public List<MovieTheater> MovieTheatersMovies { get; set; }
    public List<Actor> MovieActors { get; set; }
}