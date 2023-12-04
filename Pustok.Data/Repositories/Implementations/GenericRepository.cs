using Microsoft.EntityFrameworkCore;
using Pustokk.DAL;
using Pustokk.Models;
using Pustokk.Repositories.Interfaces;

namespace Pustokk.Repositories.Implementations
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;   
        }

        public DbSet<TEntity> Table => _context.Set<TEntity>();

        public async Task<int> CommitAsync()
        {
          return  await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            Table.Remove(entity);
        }

        public async Task<List<TEntity>> GetAllAsync(params string[]? includes)
        {
            var query = Table.AsQueryable();
            if(includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await Table.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
