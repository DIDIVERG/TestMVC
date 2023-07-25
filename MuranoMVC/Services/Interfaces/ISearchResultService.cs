using WebApplication10.DataAccessLayer.Models;

namespace WebApplication10.Services.Interfaces;

public interface ISearchResultService
{
    public Task<int> InsertResultAsync(IEnumerable<SearchResult> results);

}