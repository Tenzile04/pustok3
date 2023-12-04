using Pustokk.Models;
using Pustokk.Repositories.Interfaces;
using Pustokk.Services.Interfaces;

namespace Pustokk.Services.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public async Task CreateAsync(Genre entity)
        {
            if (_genreRepository.Table.Any(x => x.Name == entity.Name))
                throw new NullReferenceException();
            await _genreRepository.CreateAsync(entity);
            await _genreRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _genreRepository.GetByIdAsync(id);
            if (entity == null) throw new NullReferenceException();
             _genreRepository.Delete(entity);
            await _genreRepository.CommitAsync();
        }

        public async Task<List<Genre>> GetAllAsync()
        {
            return await _genreRepository.GetAllAsync();
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            var entity = await _genreRepository.GetByIdAsync(id);
            if (entity == null) throw new NullReferenceException();
            return entity;
        }

        public async Task UpdateAsync(Genre genre)
        {
            var existEntity = await _genreRepository.GetByIdAsync(genre.Id);
            if (_genreRepository.Table.Any(x => x.Name == genre.Name && existEntity.Id!=genre.Id ))
                throw new NullReferenceException();
            existEntity.Name = genre.Name;
            await _genreRepository.CommitAsync();
        }
    }
}
