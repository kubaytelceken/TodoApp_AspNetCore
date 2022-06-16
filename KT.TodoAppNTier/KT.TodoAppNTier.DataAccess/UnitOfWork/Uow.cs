using KT.TodoAppNTier.DataAccess.Contexts;
using KT.TodoAppNTier.DataAccess.Interfaces;
using KT.TodoAppNTier.DataAccess.Repositories;
using KT.TodoAppNTier.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.TodoAppNTier.DataAccess.UnitOfWork
{
    public class Uow : IUow
    {
        private readonly TodoContext _todoContext;

        public Uow(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new Repository<T>(_todoContext);
        }

        public async Task SaveChanges()
        {
            await _todoContext.SaveChangesAsync();
        }
    }
}
