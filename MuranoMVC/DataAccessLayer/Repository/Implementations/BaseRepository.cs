using Microsoft.EntityFrameworkCore;
using WebApplication10.DataAccessLayer.Repository.Interfaces;

namespace WebApplication10.DataAccessLayer.Repository.Implementations;

public abstract class BaseRepository <TEntity>: IBaseRepository<TEntity> where TEntity : class
{ 
    protected readonly SearchContext Context;
    private readonly DbSet<TEntity> _dbSet;
    protected BaseRepository(SearchContext context)
    {
        Context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<int> SaveChangesAsync()
    {
        try
        {
            return await Context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public virtual async Task<bool> CheckExistenceAsync(TEntity? entity)
    {
        var entityValue = await _dbSet.FirstOrDefaultAsync(e => e == entity);
        return entityValue != null;
    }

}