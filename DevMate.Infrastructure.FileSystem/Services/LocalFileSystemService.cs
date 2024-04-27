using DevMate.Application.Abstractions.FileSystem;
using Microsoft.AspNetCore.Hosting.Server;

namespace DevMate.Infrastructure.FileSystem.Services;

public class LocalFileSystemService : IFileSystem
{
    private const string BasePath = "./wwwroot";
    private const string Url = "http://localhost:5207";

    public async Task<string> WriteFile(Stream file, string fileName)
    {
        string filePath = BasePath + fileName;
        FileStream fs = File.Create(filePath);
        await file.CopyToAsync(fs);
        fs.Close();
        
        string fileUrl = $"{Url}/{fileName}";

        return fileUrl;
    }

    public Task<bool> Exists(string fileName)
    {
        return Task.FromResult(File.Exists(BasePath + fileName));
    }
}