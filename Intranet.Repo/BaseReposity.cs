using Intranet.Contract;
using Intranet.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Repo;

public abstract class BaseRepository<T> : IRepositoryBase<T> where T : class
{
    protected readonly IntranetContext RepositoryContext;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(IntranetContext repositoryContext)
    {
        RepositoryContext = repositoryContext;
        _dbSet = RepositoryContext.Set<T>();
    }

    public virtual IQueryable<T> FindAll(Expression<Func<T, bool>>? predicate = null)
        => _dbSet.WhereIf(predicate != null, predicate!);

    public virtual async Task<T?> FindByIdAsync(int id, CancellationToken cancellationToken)
    {
        var item = await RepositoryContext.FindAsync<T>(new object[] { id }, cancellationToken);
        return item;
    }


    public virtual async Task<T?> FindByIdAsync(string id, CancellationToken cancellationToken)
    {
        var item = await RepositoryContext.FindAsync<T>(new object[] { id }, cancellationToken);
        return item;
    }

    public void Create(T entity)
      => _dbSet.Add(entity);
    public void Update(T entity)
      => _dbSet.Update(entity);
    public void Delete(T entity)
      => _dbSet.Remove(entity);

    public async Task CreateAsync(T entity, CancellationToken cancellationToken)
        => await _dbSet.AddAsync(entity, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken)
        => RepositoryContext.SaveChangesAsync(cancellationToken);
}