using System.Collections;
using Newtonsoft.Json.Linq;
using SerpApi;
using WebApplication10.DataAccessLayer.Models;
using WebApplication10.Services.Interfaces;

namespace WebApplication10.Services.Implementations;
public class YandexService : BaseSearcher, IYandexService
{
    private readonly IConfiguration _configuration;
    public YandexService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public override async Task<IEnumerable<SearchResult>> Search(string query)
    {
        Hashtable ht = new Hashtable
        {
            { "engine", "yandex" },
            { "text", query },
            {"count", "10"}
        };
        GoogleSearch search = new GoogleSearch(ht, _configuration.GetSection("Api:SerpApi:ApiKey").Value);
        var result = await Task.Run(() => search.GetJson());
        return Parse(result);
    }
}