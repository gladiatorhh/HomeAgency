using HomeAgency.Application.Common.Interfaces;
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
    }

    public IVillaRepository Villa { get; private set; }

    public IVillaNumberRepository VillaNumber { get; private set; }

    public void Save()
    {
        _context.SaveChanges();
    }
}
