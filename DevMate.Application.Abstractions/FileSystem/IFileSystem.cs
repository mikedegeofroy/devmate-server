namespace DevMate.Application.Abstractions.FileSystem;

public interface IFileSystem
{
    Task<string> WriteFile(Stream file, string path);

    bool Exists(string path);
}