﻿using MoviesAPI.Services;

namespace MoviesAPI.Helpers;

public class InAppStorageService : IFileStorageService
{
    private IWebHostEnvironment _env;
    private IHttpContextAccessor _httpContextAccessor;
    
    public InAppStorageService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    {
        _env = env;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public Task DeleteFile(string fileRoute, string containerName)
    {
        if (string.IsNullOrEmpty(fileRoute))
            return Task.CompletedTask;
        var fileName = Path.GetFileName(fileRoute);
        var directory = Path.Combine(_env.WebRootPath, containerName, fileName);
        if (File.Exists(directory))
        {
            File.Delete(directory);
        }

        return Task.CompletedTask;
    }

    public async Task<string> SaveFile(string containerName, IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{extension}";
        string folder = Path.Combine(_env.WebRootPath, containerName);
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
        string route = Path.Combine(folder, fileName);
        using (var ms = new MemoryStream())
        {
            await file.CopyToAsync(ms);
            var content = ms.ToArray();
            await File.WriteAllBytesAsync(route, content);
        }

        var url =
            $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
        var routeForDb = Path.Combine(url, containerName, fileName).Replace("\\", "/");
        return routeForDb;
    }

    public async Task<string> EditFile(string containerName, IFormFile file, string fileRoute)
    {
        await DeleteFile(fileRoute, containerName);
        return await SaveFile(containerName, file);
    }
}