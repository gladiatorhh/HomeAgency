using HomeAgency.Domain.Entities;
using System.Linq.Expressions;

namespace HomeAgency.Application.Common.Interfaces;

public interface IVillaNumberRepository : IRepository<VillaNumber>
{
    void Update(VillaNumber villa);
}