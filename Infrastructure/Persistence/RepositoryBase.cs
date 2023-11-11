﻿using System.Linq.Expressions;
using Mc2.CrudTest.Domain.SeedWork;
using Mc2.CrudTest.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infrastructure.Persistence;

public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot
{
    protected readonly CustomerDbContext DbContext;

    public RepositoryBase(CustomerDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public IUnitOfWork UnitOfWork => DbContext;

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        DbContext.Set<TEntity>().Update(entity);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var entry = DbContext.Remove(entity);

        DbContext.Update(entity);

        return Task.CompletedTask;
    }

    public Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return DbContext.Set<TEntity>().AsNoTracking()
            .Apply(ConfigureInclude)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)!;
    }

    public Task<TEntity> GetAsync(Guid id, params Expression<Func<TEntity, object>>[] includeExpressions)
    {
        var query = DbContext.Set<TEntity>().AsQueryable().AsNoTracking();
        foreach (var include in includeExpressions) query = query.Include(include);

        return query.FirstOrDefaultAsync(x => x.Id == id)!;
    }

    protected virtual IQueryable<TEntity> ConfigureInclude(IQueryable<TEntity> query)
    {
        return query;
    }

    public TEntity Add(TEntity entity)
    {
        return DbContext.Set<TEntity>().Add(entity).Entity;
    }

    public TEntity Update(TEntity entity)
    {
        return DbContext.Set<TEntity>().Update(entity).Entity;
    }
}