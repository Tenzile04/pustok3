using Pustokk.DAL;
using Pustokk.Models;
using Pustokk.Repositories.Interfaces;
namespace Pustokk.Repositories.Implementations
{
    public class BookTagRepository : GenericRepository<BookTag>, IBookTagRepository
    {
        public BookTagRepository(AppDbContext context) : base(context) { }
    }
}
