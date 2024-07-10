using HomeAgency.Application.Common.Interfaces;
using HomeAgency.Domain.Entities;
using HomeAgency.Infrastructure.Data;
using System.Linq.Expressions;

namespace HomeAgency.Infrastructure.Common.Impelementations;

public class ApplicationUserReposotory : Repository<ApplicationUser>,IApplicaitonUserRepository
{
    private readonly HomeAgencyDbContext _context;

    public ApplicationUserReposotory(HomeAgencyDbContext context):base(context)
    {
        _context = context;
    }
}
