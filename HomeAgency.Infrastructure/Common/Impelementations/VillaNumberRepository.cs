using HomeAgency.Application.Common.Interfaces;
using HomeAgency.Domain.Entities;
using HomeAgency.Infrastructure.Data;

namespace HomeAgency.Infrastructure.Common.Impelementations;

public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
{
    private readonly HomeAgencyDbContext _context;

    public VillaNumberRepository(HomeAgencyDbContext context) : base(context)
    {
        _context = context;
    }

    public void Update(VillaNumber villa)
    {
        _context.Update(villa);
    }
}
