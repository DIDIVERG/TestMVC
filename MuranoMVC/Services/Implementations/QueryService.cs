using WebApplication10.DataAccessLayer.Models;
using WebApplication10.DataAccessLayer.Repository.Implementations;
using WebApplication10.DataAccessLayer.Repository.Interfaces;
using WebApplication10.Exceptions;
using WebApplication10.Services.Interfaces;

namespace WebApplication10.Services.Implementations;

public class QueryService : IQueryService
{
    private readonly IQueryRepository _queryRepository;
    public QueryService(IQueryRepository queryRepository)
    {
        _queryRepository = queryRepository;
    }

    public async Task<int> InsertQueryAsync(IEnumerable<Query> queries) 
        => await _queryRepository.InsertQueryAsync(queries);
    
    public async Task<Query?> GetQueryByHashAsync(string hash)
    {
        var query =  await _queryRepository.GetQueryByHashAsync(hash);
        return query;
    }
    
    public async Task<bool> CheckExistenceAsync(Query? query) => await _queryRepository.CheckExistenceAsync(query);

}