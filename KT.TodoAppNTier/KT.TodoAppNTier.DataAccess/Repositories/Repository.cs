using KT.TodoAppNTier.DataAccess.Contexts;
using KT.TodoAppNTier.DataAccess.Interfaces;
using KT.TodoAppNTier.Entities.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KT.TodoAppNTier.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly TodoContext _todoContext;

        public Repository(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        public async Task Create(T entity)
        {
           await _todoContext.Set<T>().AddAsync(entity);
        }

        public async Task<List<T>> GetAll()
        {


            try
            {
                return await _todoContext.Set<T>().AsNoTracking().ToListAsync();
            }
            catch (Exception e)
            {

                var hata = e.ToString();
                return await _todoContext.Set<T>().AsNoTracking().ToListAsync();
            }


        }

        public async Task<T> GetByFilter(Expression<Func<T, bool>> filter, bool asNoTracking = false)
        {
            return asNoTracking ? await _todoContext.Set<T>().SingleOrDefaultAsync(filter) : await _todoContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter);
        }

        public async Task<T> GetById(object id)
        {
            return await _todoContext.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetQuery()
        {
            return _todoContext.Set<T>().AsQueryable();
        }

        public void Remove(T Entity)
        {
           // var deletedEntity = _todoContext.Set<T>().Find(id);
            _todoContext.Set<T>().Remove(Entity);
        }

        public void Update(T entity ,T unchanged)
        {
           // var updatedEntity = _todoContext.Set<T>().Find(entity.Id);
            _todoContext.Entry(unchanged).CurrentValues.SetValues(entity);
            //_todoContext.Set<T>().Update(entity);
        }
    }
}
