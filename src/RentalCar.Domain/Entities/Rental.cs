using RentalCar.Domain.Exceptions;
using RentalCar.Domain.ValueObjects;

namespace RentalCar.Domain.Entities;

/// <summary>
/// 租用實體 - 負責車輛租用的領域邏輯
/// </summary>
public sealed class Rental
{
    public Guid RentalId { get; private set; }
    public Guid AccountId { get; private set; }
    public VehicleType VehicleType { get; private set; }
    public RentalPeriod Period { get; private set; }
    public Money TotalFee { get; private set; }
    public RentalStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ConfirmedAt { get; private set; }

    private Rental(Guid rentalId, Guid accountId)
    {
        RentalId = rentalId;
        AccountId = accountId;
        Status = RentalStatus.Draft;
        CreatedAt = DateTime.UtcNow;
        TotalFee = Money.Zero;
    }

    /// <summary>
    /// 開始租車流程
    /// </summary>
    public static Rental ToRentalCar(Guid accountId) =>
        new(Guid.NewGuid(), accountId);

    /// <summary>
    /// 設定租用期間
    /// </summary>
    public void SetRentalPeriod(DateOnly startDate, DateOnly endDate)
    {
        if (Status != RentalStatus.Draft)
            throw new DomainException("只有草稿狀態可以設定租用期間");

        Period = RentalPeriod.Create(startDate, endDate);
    }

    /// <summary>
    /// 選擇車型
    /// </summary>
    public void SelectVehicleType(VehicleType vehicleType)
    {
        if (Status != RentalStatus.Draft)
            throw new DomainException("只有草稿狀態可以選擇車型");

        VehicleType = vehicleType ?? throw new ArgumentNullException(nameof(vehicleType));
    }

    /// <summary>
    /// 計算租金
    /// </summary>
    public void CalculateFee()
    {
        if (VehicleType == null)
            throw new DomainException("必須先選擇車型");

        if (Period == null)
            throw new DomainException("必須先設定租用期間");

        var totalAmount = VehicleType.DailyRate * Period.Days;
        TotalFee = Money.FromDecimal(totalAmount);
    }

    /// <summary>
    /// 顯示租用詳情
    /// </summary>
    public string ShowRentalDetails() =>
        $"車型：{VehicleType?.Name}\n" +
        $"期間：{Period}\n" +
        $"租金：{TotalFee}";

    /// <summary>
    /// 確認租用
    /// </summary>
    public void ConfirmRental()
    {
        if (Status != RentalStatus.Draft)
            throw new DomainException("只有草稿狀態可以確認");

        if (VehicleType == null || Period == null || TotalFee == Money.Zero)
            throw new DomainException("租用資訊不完整");

        Status = RentalStatus.Confirmed;
        ConfirmedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// 租用完成
    /// </summary>
    public void RentalCompleted()
    {
        if (Status != RentalStatus.Confirmed)
            throw new DomainException("只有已確認的租用可以完成");

        Status = RentalStatus.Completed;
    }
}

public enum RentalStatus
{
    Draft,
    Confirmed,
    Completed,
    Cancelled
}
