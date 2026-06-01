using RentalCar.Application.Abstractions;

namespace RentalCar.Application.UseCases.Accounts.Register;

public sealed record RegisterAccountCommand(
    string Username,
    string Password,
    string Email
) : ICommand<Guid>;
