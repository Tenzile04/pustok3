using Microsoft.EntityFrameworkCore;
using Pustokk.Models;

namespace Pustokk.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity>where TEntity:BaseEntity,new()
    {
        public DbSet<TEntity> Table { get;  }
        Task CreateAsync(TEntity entity);
        void Delete(TEntity entity);
      
        Task <TEntity> GetByIdAsync(int id);
        Task<List<TEntity>> GetAllAsync(params string[]? includes);
        Task<int> CommitAsync();

    }
}
