using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Entities;

namespace MoviesAPI.Controllers;
[Route("api/genres")]
public class GenresController : Controller
{
    private readonly IRepository _repository;
    
    public GenresController(IRepository repository)
    {
        _repository = repository;
    }
    // GET
    [HttpGet]
    public List<Genre> Index()
    {
        return _repository.GetAllGenres();
    }
}