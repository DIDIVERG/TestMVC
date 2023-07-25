using Microsoft.EntityFrameworkCore;
using SerpApi;
using WebApplication10.DataAccessLayer;
using WebApplication10.DataAccessLayer.Repository.Implementations;
using WebApplication10.DataAccessLayer.Repository.Interfaces;
using WebApplication10.Services.Implementations;
using WebApplication10.Services.Interfaces;

namespace WebApplication10.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterServices(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddDbContext<SearchContext>(opt => opt
            .UseSqlServer(configuration.GetConnectionString("Default")).UseSnakeCaseNamingConvention());
        collection.AddScoped<IQueryRepository, QueryRepository>();
        collection.AddScoped<ISearchResultRepository, SearchResultRepository>();
        collection.AddScoped<IQueryService, QueryService>();
        collection.AddScoped<IYandexService, YandexService>();
        collection.AddScoped<IBingService, BingService>();
        collection.AddScoped<IGoogleService, GoogleService>();
        collection.AddTransient<IHashService, HashService>();
        return collection;
    }
}