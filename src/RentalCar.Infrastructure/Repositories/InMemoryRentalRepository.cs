using RentalCar.Application.Abstractions.Repositories;
using RentalCar.Domain.Entities;

namespace RentalCar.Infrastructure.Repositories;

/// <summary>
/// InMemory 租用儲存庫實作（Adapter）
/// </summary>
public sealed class InMemoryRentalRepository : IRentalRepository
{
    private readonly List<Rental> _store = new();

    public Task<Rental?> GetByIdAsync(Guid rentalId, CancellationToken cancellationToken = default)
    {
        var rental = _store.FirstOrDefault(r => r.RentalId == rentalId);
        return Task.FromResult(rental);
    }

    public Task<IReadOnlyList<Rental>> GetByAccountIdAsync(
        Guid accountId, 
        CancellationToken cancellationToken = default)
    {
        var rentals = _store
            .Where(r => r.AccountId == accountId)
            .ToList();
        return Task.FromResult<IReadOnlyList<Rental>>(rentals);
    }

    public Task AddAsync(Rental rental, CancellationToken cancellationToken = default)
    {
        _store.Add(rental);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Rental rental, CancellationToken cancellationToken = default)
    {
        var existing = _store.FirstOrDefault(r => r.RentalId == rental.RentalId);
        if (existing is not null)
        {
            _store.Remove(existing);
            _store.Add(rental);
        }
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        Task.CompletedTask; // InMemory 不需要持久化
}
