using Microsoft.EntityFrameworkCore;
using Server.Date;
using Server.Exceptions;
using Server.Repo.interfaces;

namespace Server.Repo.repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
           var entity =await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException($"{typeof(T).Name}is not found");
            }
            _context.Set<T>().Remove(entity);
                return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities =await _context.Set<T>()
                    .AsNoTracking().ToListAsync();
            return entities;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var entity =await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException($"{typeof(T).Name} is not found");
            }
            return entity;
        }

        public async Task<int> UpdateAsync(T entity)
        {
           _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync();
        }
    
        public IQueryable<T> Query()
        {
            return _context.Set<T>();
        }
    
    }

}
