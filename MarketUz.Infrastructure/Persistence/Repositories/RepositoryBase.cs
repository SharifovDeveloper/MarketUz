using MarketUz.Domain.Common;
using MarketUz.Domain.Exceptions;
using MarketUz.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MarketUz.Infrastructure.Persistence.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected readonly MarketUzDbContext _context;

        public RepositoryBase(MarketUzDbContext context)
        {
            _context = context;
        }

        public  T Create(T entity)
        {
            var createdEntity = _context.Set<T>().Add(entity);

            return createdEntity.Entity;
        }

        public void Delete(int id)
        {
            var entity =  FindById(id);

            _context.Set<T>().Remove(entity);
        }

        public IEnumerable<T> FindAll()
        {
            var entities =  _context.Set<T>()
                .AsNoTracking()
                .ToList();

            return entities;
        }

        public T FindById(int id)
        {
            var entity = _context.Set<T>()
                .Find(id);

            if (entity is null)
            {
                throw new EntityNotFoundException(
                    $"Entity {typeof(T)} with id: {id} not found.");
            }

            return entity;
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}