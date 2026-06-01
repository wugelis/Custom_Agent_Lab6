using RentalCar.Application.Abstractions;

namespace RentalCar.Application.UseCases.Accounts.Login;

public sealed record LoginCommand(
    string Username,
    string Password
) : ICommand<LoginResult>;

public sealed record LoginResult(
    bool Success,
    Guid? AccountId,
    string? Message
);
