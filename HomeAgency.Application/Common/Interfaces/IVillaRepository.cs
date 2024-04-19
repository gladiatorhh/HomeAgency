using HomeAgency.Domain.Entities;
using System.Linq.Expressions;

namespace HomeAgency.Application.Common.Interfaces;

public interface IVillaRepository
{
    void Update(Villa villa);
    void SaveChanges();
}