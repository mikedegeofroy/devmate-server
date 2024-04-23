using DevMate.Application.Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DevMate.Infrastructure.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDataAccess(
        this IServiceCollection collection)
    {
        collection.AddSingleton<IUserCodeRepository, UserCodeRepository>();

        return collection;
    }
}