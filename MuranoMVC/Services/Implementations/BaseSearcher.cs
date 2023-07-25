using Newtonsoft.Json.Linq;
using WebApplication10.DataAccessLayer.Models;
using WebApplication10.Services.Interfaces;

namespace WebApplication10.Services.Implementations;

public abstract class BaseSearcher : ISearcher
{
    public abstract Task<IEnumerable<SearchResult>> Search(string query);

    protected IEnumerable<SearchResult> Parse(JObject objectResult)
    {
        List<SearchResult> searchResults = new List<SearchResult>();

        if (objectResult.TryGetValue("organic_results", out var organicResults))
        {
            if (organicResults is JArray organicResultsArray)
            {
                int count = Math.Min(10, organicResultsArray.Count);
                for (int i = 0; i < count; i++)
                {
                    var organicResult = organicResultsArray[i];
                    string link = organicResult.Value<string>("link");
                    string snippet = organicResult.Value<string>("snippet");
                    SearchResult searchResult = new SearchResult
                    {
                        Url = link,
                        Snippet = snippet
                    };
                    searchResults.Add(searchResult);
                }
            }
        }
        return searchResults;
    }
}