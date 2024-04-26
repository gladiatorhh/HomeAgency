using HomeAgency.Application.Common.Interfaces;
using HomeAgency.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HomeAgency.Infrastructure.Common.Impelementations;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly HomeAgencyDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(HomeAgencyDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public void Add(T T)
    {
        _dbSet.Add(T);
    }

    public bool Any(Expression<Func<T, bool>> filter)
    {
        return _dbSet.Any(filter);
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
    {
        IQueryable<T> items = _dbSet;
        
        if(filter is not null)
            items = items.Where(filter);

        if (includeProperties != null)
        {
            foreach (string include in includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries))
            {
                items = items.Include(include);
            }
        }

        return items.ToList();
    }

    public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
    {
        IQueryable<T> items = _dbSet;

        if (includeProperties != null)
        {
            foreach (string include in includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries))
            {
                items = items.Include(include);
            }
        }

        return items.FirstOrDefault(filter);
    }

    public void Remove(T T)
    {
       _dbSet.Remove(T);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}
