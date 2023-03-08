using Microsoft.EntityFrameworkCore;
using MoviesAPI;
using MoviesAPI.APIBehaviour;
using MoviesAPI.Filters;
using MoviesAPI.Helpers;
using MoviesAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ParseBadRequest));
}).ConfigureApiBehaviorOptions(BadRequestBehaviour.Parse);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IRepository, MoviesSQLServerRepository>();
builder.Services.AddScoped<MoviesDataService>();
builder.Services.AddScoped<IFileStorageService, InAppStorageService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<MoviesDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetValue<string>("DefaultConnection")));

builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    var frontendURL = builder.Configuration.GetValue<string>("frontend_url");
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader()
            .WithExposedHeaders(new string[] { "totalAmountOfRecords" });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.UseCors();

app.MapControllers();

app.Run();