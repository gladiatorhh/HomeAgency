namespace HomeAgency.Application.Common.Interfaces;

public interface IUnitOfWork
{
    public IVillaRepository Villa { get; }
    public IVillaNumberRepository VillaNumber { get; }
    void Save();
}
