using Microsoft.EntityFrameworkCore;
using WebApplication10.DataAccessLayer.Models;
using WebApplication10.DataAccessLayer.Repository.Interfaces;

namespace WebApplication10.DataAccessLayer.Repository.Implementations;

public class SearchResultRepository : BaseRepository<SearchResult>, ISearchResultRepository
{
    public SearchResultRepository(SearchContext context) : base(context)
    {
    }
    
    public async Task<int> InsertResultAsync(IEnumerable<SearchResult> results)
    {
        await Context.Results.AddRangeAsync(results);
        return await SaveChangesAsync();
    }
    
}