using HomeAgency.Application.Common.Interfaces;
using HomeAgency.Infrastructure.Data;
using System.Linq.Expressions;

namespace HomeAgency.Infrastructure.Common.Impelementations;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly HomeAgencyDbContext _context;

    public Repository(HomeAgencyDbContext context)
    {
        _context = context;
    }

    public void Add(T T)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
    {
        throw new NotImplementedException();
    }

    public T GetT(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
    {
        throw new NotImplementedException();
    }

    public void Remove(T T)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
    {
        throw new NotImplementedException();
    }
}
