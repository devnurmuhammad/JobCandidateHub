using JCHub.Application.DbInterfaces;
using JCHub.Application.Implements.Services;
using JCHub.Application.IRepositories;
using JCHub.Application.IServices;
using JCHub.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JCHub.Infrastructure.Dependencies;

public static class DependencyGroup
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services
            .AddScoped<IUnitOfWork, UnitOfWork>()    
            .AddScoped<ICandidateRepository, CandidateRepository>()
            .AddScoped<ICandidateService, CandidateService>()
    
            .AddMemoryCache()
            ;
        
        return services;
    }
}