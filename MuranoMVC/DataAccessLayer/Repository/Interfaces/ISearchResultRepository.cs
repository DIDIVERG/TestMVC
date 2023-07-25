using WebApplication10.DataAccessLayer.Models;

namespace WebApplication10.DataAccessLayer.Repository.Interfaces;

public interface ISearchResultRepository
{
    public Task<int> InsertResultAsync(IEnumerable<SearchResult> results);
    public  Task<bool> CheckExistenceAsync(SearchResult entity);
}