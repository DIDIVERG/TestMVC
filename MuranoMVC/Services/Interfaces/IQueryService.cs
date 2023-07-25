using WebApplication10.DataAccessLayer.Models;

namespace WebApplication10.Services.Interfaces;

public interface IQueryService
{
    public Task<int> InsertQueryAsync(IEnumerable<Query> queries);
    public Task<Query?> GetQueryByHashAsync(string hash);
    public Task<bool> CheckExistenceAsync(Query? entity);

}