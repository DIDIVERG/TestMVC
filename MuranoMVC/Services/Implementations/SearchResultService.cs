using WebApplication10.DataAccessLayer;
using WebApplication10.DataAccessLayer.Models;
using WebApplication10.DataAccessLayer.Repository.Implementations;
using WebApplication10.DataAccessLayer.Repository.Interfaces;
using WebApplication10.Services.Interfaces;

namespace WebApplication10.Services.Implementations;

public class SearchResultService : ISearchResultService
{
    private readonly ISearchResultRepository _resultRepository;
    public SearchResultService(ISearchResultRepository resultRepository)
    {
        _resultRepository = resultRepository;
    }
    public async Task<int> InsertResultAsync(IEnumerable<SearchResult> results)
    {
        var newResults = new List<SearchResult>();
        foreach (var result in results)
        {
            bool exists = await _resultRepository.CheckExistenceAsync(result);
            if (!exists)
            {
                newResults.Add(result);
            }
        }
        await _resultRepository.InsertResultAsync(newResults);
        return newResults.Count;
    }
}