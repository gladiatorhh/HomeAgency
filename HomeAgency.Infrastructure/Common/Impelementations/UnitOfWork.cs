using HomeAgency.Application.Common.Interfaces;
using HomeAgency.Domain.Entities;
using HomeAgency.Infrastructure.Data;

namespace HomeAgency.Infrastructure.Common.Impelementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly HomeAgencyDbContext _context;

    public UnitOfWork(HomeAgencyDbContext context)
    {
        _context = context;
        Villa = new VillaRepository(context);
        VillaNumber = new VillaNumberRepository(context);
        Amenity = new AmenityRepository(context);
    }

    public IVillaRepository Villa { get; private set; }

    public IVillaNumberRepository VillaNumber { get; private set; }

    public IAmenityRepository Amenity { get; private set; }

    public void Save()
    {
        _context.SaveChanges();
    }
}
