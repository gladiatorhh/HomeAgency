namespace HomeAgency.Application.Common.Interfaces;

public interface IUnitOfWork
{
    public IVillaRepository Villa { get; }
    public IVillaNumberRepository VillaNumber { get; }
    public IAmenityRepository Amenity { get; }
    void Save();
}
