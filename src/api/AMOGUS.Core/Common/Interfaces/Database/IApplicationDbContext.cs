namespace AMOGUS.Core.Common.Interfaces.Database
{
    public interface IApplicationDbContext
    {

        // TODO: DbSets definieren
        // -> ER- Diagramm aufsetzen

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();

        Task<bool> EnsureDatabaseAsync();
        Task MigrateDatabaseAsync();

    }
}
