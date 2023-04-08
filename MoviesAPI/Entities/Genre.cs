using System.ComponentModel.DataAnnotations;
using MoviesAPI.Validations;

namespace MoviesAPI.Entities;

public class Genre
{
    public int Id { get; set; }
    [Required]
    [StringLength(30)]
    [FirstLetterUppercase]
    public string Name { get; set; }

    public List<Movie> Movies { get; set; } = new List<Movie>();
}