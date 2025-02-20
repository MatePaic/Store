﻿using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Infrastructure.Data
{
    public class GenericRepository<T>(StoreContext context) : IGenericRepository<T> where T : BaseEntity
    {
        async Task<IReadOnlyList<T>> IGenericRepository<T>.GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetWithSpecificationAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public async Task<IReadOnlyList<TResult>> GetWithSpecificationAsync<TResult>(ISpecification<T, TResult> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }
        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<T?> GetFirstOrDefaultWithSpecAsync(ISpecification<T> specification, Expression<Func<T, bool>> predicate)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync(predicate);
        }

        public async Task<T?> GetFirstOrDefaultWithSpecAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public bool Exists(int id)
        {
            return context.Set<T>().Any(x => x.Id == id);
        }

        public bool HasChanges()
        {
            return context.ChangeTracker.HasChanges();
        }

        public async Task<int> CountAsync(ISpecification<T> specification)
        {
            var query = context.Set<T>().AsQueryable();

            query = specification.ApplyCriteria(query);

            return await query.CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            return SpecificationEvaulator<T>.GetQuery(context.Set<T>().AsQueryable(), specification);
        }

        private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification)
        {
            return SpecificationEvaulator<T>.GetQuery<T, TResult>(context.Set<T>().AsQueryable(), specification);
        }
    }
}
