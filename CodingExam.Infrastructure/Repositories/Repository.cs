using CodingExam.Domain.Interfaces;
using CodingExam.Domain.Models;
using CodingExam.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodingExam.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly CodingExamContext Db;

        protected readonly DbSet<T> DbSet;

        public Repository(CodingExamContext db)
        {
            Db = db;
            DbSet = db.Set<T>();
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task Add(T entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Update(T entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            //DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Delete(T entity)
        {
            DbSet.Remove(entity);
            await SaveChanges();
        }

        public virtual async Task<List<T>> Search(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }
    }
}
