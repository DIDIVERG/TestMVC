using System.Collections;
using SerpApi;
using WebApplication10.DataAccessLayer.Models;
using WebApplication10.Services.Interfaces;

namespace WebApplication10.Services.Implementations;

public class GoogleService : BaseSearcher, IGoogleService
{
    private readonly IConfiguration _configuration;

    public GoogleService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public override async Task<IEnumerable<SearchResult>> Search(string query)
    {
        Hashtable ht = new Hashtable
        {
            { "engine", "google" },
            { "q", query },
            {"count", "10"}
        };
        GoogleSearch search = new GoogleSearch(ht, _configuration.GetSection("Api:SerpApi:ApiKey").Value);
        var result = await Task.Run(() => search.GetJson());
        return Parse(result);
    }
}