using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Entities;

namespace MoviesAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GenresController : Controller
{
    private readonly IRepository _repository;
    
    public GenresController(IRepository repository)
    {
        _repository = repository;
    }
    // GET
    [HttpGet]
    public ActionResult<List<Genre>> GetAllGenres()
    {
        return _repository.GetAllGenres();
    }

    [HttpGet("{id:int}")]
    public ActionResult<Genre> Get(int id)
    {
        var genre = _repository.GetGenreById(id);
        if (genre == null)
        {
            return NotFound();
        }

        return genre;
    }

    [HttpPost]
    public void Post()
    {
        
    }

    [HttpPut]
    public void Put()
    {
        
    }

    [HttpDelete]
    public void Delete()
    {
        
    }
}