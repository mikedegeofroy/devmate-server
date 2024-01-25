using DevMate.Application.Abstractions.FileSystem;

namespace DevMate.Infrastructure.FileSystem.Services;

public class LocalFileSystemService : IFileSystem
{
    private const string BasePath = "./wwwroot";

    public async Task<string> WriteFile(Stream file, string path)
    {
        string filePath = BasePath + path;
        FileStream fs = File.Create(filePath);
        await file.CopyToAsync(fs);
        fs.Close();

        return path;
    }

    public bool Exists(string path)
    {
        return File.Exists(BasePath + path);
    }
}