using RentalCar.Domain.Entities;

namespace RentalCar.Application.Abstractions.Repositories;

/// <summary>
/// 租用儲存庫介面（Outbound Port）
/// </summary>
public interface IRentalRepository
{
    Task<Rental?> GetByIdAsync(Guid rentalId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Rental>> GetByAccountIdAsync(Guid accountId, CancellationToken cancellationToken = default);
    Task AddAsync(Rental rental, CancellationToken cancellationToken = default);
    Task UpdateAsync(Rental rental, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
