using RentalCar.Application.Abstractions;
using RentalCar.Application.Abstractions.Repositories;

namespace RentalCar.Application.UseCases.Accounts.Login;

public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResult>
{
    private readonly IAccountRepository _accountRepository;

    public LoginCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<LoginResult> HandleAsync(
        LoginCommand command, 
        CancellationToken cancellationToken = default)
    {
        var account = await _accountRepository
            .GetByUsernameAsync(command.Username, cancellationToken);

        if (account is null)
        {
            return new LoginResult(false, null, "帳號或密碼錯誤");
        }

        var success = account.Login(command.Password);

        if (success)
        {
            await _accountRepository.SaveChangesAsync(cancellationToken);
            return new LoginResult(true, account.AccountId, "登入成功");
        }

        return new LoginResult(false, null, "帳號或密碼錯誤");
    }
}
