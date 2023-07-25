using Newtonsoft.Json.Linq;
using WebApplication10.DataAccessLayer.Models;

namespace WebApplication10.Services.Interfaces;

public interface ISearcher
{
    public Task<IEnumerable<SearchResult>> Search(string query);
}