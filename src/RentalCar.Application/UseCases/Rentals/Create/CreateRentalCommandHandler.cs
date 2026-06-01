using RentalCar.Application.Abstractions;
using RentalCar.Application.Abstractions.Repositories;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Exceptions;
using RentalCar.Domain.ValueObjects;

namespace RentalCar.Application.UseCases.Rentals.Create;

public sealed class CreateRentalCommandHandler 
    : ICommandHandler<CreateRentalCommand, CreateRentalResult>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IRentalRepository _rentalRepository;

    public CreateRentalCommandHandler(
        IAccountRepository accountRepository,
        IRentalRepository rentalRepository)
    {
        _accountRepository = accountRepository;
        _rentalRepository = rentalRepository;
    }

    public async Task<CreateRentalResult> HandleAsync(
        CreateRentalCommand command, 
        CancellationToken cancellationToken = default)
    {
        // 1. 驗證帳號存在
        var account = await _accountRepository
            .GetByIdAsync(command.AccountId, cancellationToken);
        
        if (account is null)
            throw new DomainException("帳號不存在");

        // 2. 驗證車型
        var vehicleType = VehicleType.FromName(command.VehicleTypeName);
        if (vehicleType is null)
            throw new DomainException("車型不存在");

        // 3. 開始租車流程（ToRentalCar）
        var rental = Rental.ToRentalCar(command.AccountId);

        // 4. 選擇車型
        rental.SelectVehicleType(vehicleType);

        // 5. 設定租用期間
        rental.SetRentalPeriod(command.StartDate, command.EndDate);

        // 6. 計算租金
        rental.CalculateFee();

        // 7. 確認租用
        rental.ConfirmRental();

        // 8. 儲存
        await _rentalRepository.AddAsync(rental, cancellationToken);
        await _rentalRepository.SaveChangesAsync(cancellationToken);

        // 9. 返回結果
        return new CreateRentalResult(
            rental.RentalId,
            rental.VehicleType.Name,
            rental.Period.ToString(),
            rental.TotalFee.Amount,
            "租用完成"
        );
    }
}
