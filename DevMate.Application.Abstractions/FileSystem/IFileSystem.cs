namespace DevMate.Application.Abstractions.FileSystem;

public interface IFileSystem
{
    Task<string> WriteFile(Stream file, string fileName);

    Task<bool> Exists(string fileName);
}