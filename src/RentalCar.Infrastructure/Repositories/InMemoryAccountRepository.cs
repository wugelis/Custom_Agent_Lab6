using RentalCar.Application.Abstractions.Repositories;
using RentalCar.Domain.Entities;

namespace RentalCar.Infrastructure.Repositories;

/// <summary>
/// InMemory 帳號儲存庫實作（Adapter）
/// </summary>
public sealed class InMemoryAccountRepository : IAccountRepository
{
    private readonly List<Account> _store = new();

    public Task<Account?> GetByIdAsync(Guid accountId, CancellationToken cancellationToken = default)
    {
        var account = _store.FirstOrDefault(a => a.AccountId == accountId);
        return Task.FromResult(account);
    }

    public Task<Account?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        var account = _store.FirstOrDefault(
            a => string.Equals(a.Username, username, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(account);
    }

    public Task<Account?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var account = _store.FirstOrDefault(
            a => string.Equals(a.Email, email, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(account);
    }

    public Task AddAsync(Account account, CancellationToken cancellationToken = default)
    {
        _store.Add(account);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        Task.CompletedTask; // InMemory 不需要持久化
}
