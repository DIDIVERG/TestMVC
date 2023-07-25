using WebApplication10.DataAccessLayer.Models;
using WebApplication10.DTOs;

namespace WebApplication10.Views.Search.ViewModels;

public class SearchViewModel
{
    public List<string> Words { get; set; } = new List<string>();
    public IEnumerable<SearchResultDto> SearchResults { get; set; } = new List<SearchResultDto>();

}