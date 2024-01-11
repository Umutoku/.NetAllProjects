
using Microsoft.EntityFrameworkCore;
using UdemyRealWorldUnitTest.Web.Models;

namespace UdemyRealWorldUnitTest.Web.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly UdemyUnitTestDBContext _context;
        private readonly DbSet<TEntity> _entities;
        public Repository(UdemyUnitTestDBContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }
        public async Task<TEntity> Create(TEntity entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(TEntity entity)
        {
             _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<TEntity> GetById(int id) => await _entities.FindAsync(id);

        public async Task<TEntity> Update(TEntity entity)
        {
             _context.Entry(entity).State = EntityState.Modified;
             _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
