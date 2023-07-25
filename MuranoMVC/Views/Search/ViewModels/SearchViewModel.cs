using WebApplication10.DataAccessLayer.Models;

namespace WebApplication10.Views.Search.ViewModels;

public class SearchViewModel
{
    public List<string> Words { get; set; } = new List<string>();
    public IEnumerable<SearchResult> SearchResults { get; set; } = new List<SearchResult>();

}