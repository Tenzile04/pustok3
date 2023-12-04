using Pustokk.DAL;
using Pustokk.Models;
using Pustokk.Repositories.Interfaces;

namespace Pustokk.Repositories.Implementations
{
    public class BookImageRepository : GenericRepository<BookImage>, IBookImageRepository
    {
        public BookImageRepository(AppDbContext context) : base(context)
        {
        }
    }
}
