using System.ComponentModel.DataAnnotations;
using MoviesAPI.Validations;

namespace MoviesAPI.DTOs;

public class GenreCreationDTO
{
    [Required]
    [StringLength(30)]
    [FirstLetterUppercase]
    public string Name { get; set; }
}