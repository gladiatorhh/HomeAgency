using HomeAgency.Domain.Entities;

namespace HomeAgency.Application.Common.Interfaces;

public interface IAmenityRepository : IRepository<Amenity>
{
    void Update(Amenity amenity);
}
