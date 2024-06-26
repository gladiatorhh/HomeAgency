﻿using HomeAgency.Application.Common.Interfaces;
using HomeAgency.Domain.Entities;
using HomeAgency.Infrastructure.Data;

namespace HomeAgency.Infrastructure.Common.Impelementations;

public class VillaRepository : Repository<Villa>, IVillaRepository
{
    private readonly HomeAgencyDbContext _context;

    public VillaRepository(HomeAgencyDbContext context):base(context)
    {
        _context = context;
    }

    public void SaveChanges() =>
        _context.SaveChanges();

    public void Update(Villa villa) =>
        _context.villas.Update(villa);
}
