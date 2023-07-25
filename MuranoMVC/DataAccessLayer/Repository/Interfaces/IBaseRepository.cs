namespace WebApplication10.DataAccessLayer.Repository.Interfaces;

public interface IBaseRepository <TEntity>
{
    public Task<int> SaveChangesAsync();
}