using CodingExam.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodingExam.Domain.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<List<T>> Search(Expression<Func<T, bool>> predicate);
        Task<int> SaveChanges();
    }
}
