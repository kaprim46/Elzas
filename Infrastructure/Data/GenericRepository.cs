using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T>(AppDbContext db) : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _db = db;

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_db.Set<T>().AsQueryable(), spec);
        }

        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _db.Set<T>().Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }
    }
}