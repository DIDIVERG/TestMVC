using WebApplication10.DataAccessLayer.Models;

namespace WebApplication10.DataAccessLayer.Repository.Interfaces;

public interface IQueryRepository
{
    public Task<int> InsertQueryAsync(IEnumerable<Query> queries);
    public Task<Query?> GetQueryByHashAsync(string hash);
    public Task<bool> CheckExistenceAsync(Query? entity);

}