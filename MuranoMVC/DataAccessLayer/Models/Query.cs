using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApplication10.DataAccessLayer.Models;

public class Query
{
    [Key]
    public int Id { get; set; }
    public string QueryKeywordHash { get; set; }
    [InverseProperty(nameof(SearchResult.Queries))]
    public IEnumerable<SearchResult> Results { get; set; }
}