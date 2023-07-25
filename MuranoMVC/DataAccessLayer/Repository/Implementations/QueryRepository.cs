using Microsoft.EntityFrameworkCore;
using WebApplication10.DataAccessLayer.Models;
using WebApplication10.DataAccessLayer.Repository.Interfaces;

namespace WebApplication10.DataAccessLayer.Repository.Implementations;

public class QueryRepository : BaseRepository<Query>, IQueryRepository
{
    public QueryRepository(SearchContext context) : base(context)
    {
    }
    public async Task<int> InsertQueryAsync(IEnumerable<Query> queries)
    {
       await Context.Queries.AddRangeAsync(queries);
       return await SaveChangesAsync();
    }

    public async Task<Query?> GetQueryByHashAsync(string hash)
        =>  await Context.Queries.Include(i => i.Results)
            .FirstOrDefaultAsync(q => q.QueryKeywordHash == hash);
    

    public override async Task<bool> CheckExistenceAsync(Query? entity)
    {
        return await base.CheckExistenceAsync(entity);
    }
}