using DevMate.Application.Abstractions.FileSystem;
using DevMate.Infrastructure.FileSystem.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DevMate.Infrastructure.FileSystem.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureFileSystem(this IServiceCollection collection)
    {
        collection.AddSingleton<IFileSystem, AmazonS3FileSystem>();

        return collection;
    }
}