namespace DevMate.Application.Abstractions.FileSystem;

public interface IFileSystem
{
    Task<string> WriteFile(Stream file, string fileName);

    bool Exists(string fileName);
}