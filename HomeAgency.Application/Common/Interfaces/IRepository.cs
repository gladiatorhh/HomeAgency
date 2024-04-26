using HomeAgency.Domain.Entities;
using System.Linq.Expressions;

namespace HomeAgency.Application.Common.Interfaces;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
    T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
    void Add(T T);
    void Remove(T T);
    bool Any(Expression<Func<T, bool>> filter);
    void SaveChanges();
}
