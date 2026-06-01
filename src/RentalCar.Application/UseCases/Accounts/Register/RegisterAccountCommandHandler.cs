using RentalCar.Application.Abstractions;
using RentalCar.Application.Abstractions.Repositories;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Exceptions;

namespace RentalCar.Application.UseCases.Accounts.Register;

public sealed class RegisterAccountCommandHandler 
    : ICommandHandler<RegisterAccountCommand, Guid>
{
    private readonly IAccountRepository _accountRepository;

    public RegisterAccountCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Guid> HandleAsync(
        RegisterAccountCommand command, 
        CancellationToken cancellationToken = default)
    {
        // 檢查用戶名稱是否已存在
        var existingByUsername = await _accountRepository
            .GetByUsernameAsync(command.Username, cancellationToken);
        
        if (existingByUsername is not null)
            throw new DomainException("用戶名稱已被使用");

        // 檢查電子郵件是否已存在
        var existingByEmail = await _accountRepository
            .GetByEmailAsync(command.Email, cancellationToken);
        
        if (existingByEmail is not null)
            throw new DomainException("電子郵件已被使用");

        // 建立新帳號
        var account = Account.Create(command.Username, command.Password, command.Email);

        // 儲存
        await _accountRepository.AddAsync(account, cancellationToken);
        await _accountRepository.SaveChangesAsync(cancellationToken);

        return account.AccountId;
    }
}
