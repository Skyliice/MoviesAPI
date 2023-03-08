using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Entities;

namespace MoviesAPI;

public class MoviesDbContext : DbContext
{
    public MoviesDbContext([NotNull] DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Actor> Actors { get; set; }
}