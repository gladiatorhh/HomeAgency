using HomeAgency.Domain.Entities;
using System.Linq.Expressions;

namespace HomeAgency.Application.Common.Interfaces;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
    T GetT(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
    void Add(T T);
    void Remove(T T);
    void SaveChanges();
}
