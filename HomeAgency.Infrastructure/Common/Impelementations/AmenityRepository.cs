using HomeAgency.Application.Common.Interfaces;
using HomeAgency.Domain.Entities;
using HomeAgency.Infrastructure.Data;
using System.Linq.Expressions;

namespace HomeAgency.Infrastructure.Common.Impelementations;

public class AmenityRepository : Repository<Amenity>,IAmenityRepository
{
    private readonly HomeAgencyDbContext _context;

    public AmenityRepository(HomeAgencyDbContext context):base(context)
    {
        _context = context;
    }

    void IAmenityRepository.Update(Amenity amenity)
    {
        _context.Amenity.Update(amenity);
    }
}
