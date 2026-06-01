using RentalCar.Application.Abstractions;

namespace RentalCar.Application.UseCases.Rentals.Create;

public sealed record CreateRentalCommand(
    Guid AccountId,
    string VehicleTypeName,
    DateOnly StartDate,
    DateOnly EndDate
) : ICommand<CreateRentalResult>;

public sealed record CreateRentalResult(
    Guid RentalId,
    string VehicleTypeName,
    string Period,
    decimal TotalFee,
    string Message
);
