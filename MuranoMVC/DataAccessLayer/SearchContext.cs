using Microsoft.EntityFrameworkCore;
using WebApplication10.DataAccessLayer.Models;

namespace WebApplication10.DataAccessLayer;

public class SearchContext : DbContext
{
    public DbSet<Query> Queries { get; set; }  
    public DbSet<SearchResult> Results { get; set; }
    public SearchContext(DbContextOptions<SearchContext> option) : base(option)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}