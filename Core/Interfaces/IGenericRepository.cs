﻿using Core.Entities;
using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetWithSpecificationAsync(ISpecification<T> specification);
        Task<IReadOnlyList<TResult>> GetWithSpecificationAsync<TResult>(ISpecification<T, TResult> specification);
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetFirstOrDefaultWithSpecAsync(ISpecification<T> specification, Expression<Func<T, bool>> predicate);
        Task<T?> GetFirstOrDefaultWithSpecAsync(ISpecification<T> specification);
        void Add(T entity);
        void Remove(T entity);
        bool Exists(int id);
        bool HasChanges();
        Task<int> CountAsync(ISpecification<T> specification);
    }
}
