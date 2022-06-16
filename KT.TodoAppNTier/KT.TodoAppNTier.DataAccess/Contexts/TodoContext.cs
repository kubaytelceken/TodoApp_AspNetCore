using KT.TodoAppNTier.DataAccess.Configurations;
using KT.TodoAppNTier.Entities.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.TodoAppNTier.DataAccess.Contexts
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WorkConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        DbSet<Work> Works { get; set; }
    }
}
