using RentalCar.Domain.Exceptions;

namespace RentalCar.Domain.ValueObjects;

/// <summary>
/// 租用期間值物件
/// </summary>
public sealed record RentalPeriod
{
    public DateOnly StartDate { get; }
    public DateOnly EndDate { get; }
    public int Days { get; }

    private RentalPeriod(DateOnly startDate, DateOnly endDate)
    {
        if (endDate <= startDate)
            throw new DomainException("結束日期必須晚於開始日期");

        StartDate = startDate;
        EndDate = endDate;
        Days = endDate.DayNumber - startDate.DayNumber;
    }

    public static RentalPeriod Create(DateOnly startDate, DateOnly endDate) =>
        new(startDate, endDate);

    public override string ToString() =>
        $"{StartDate:yyyy-MM-dd} ~ {EndDate:yyyy-MM-dd} ({Days} 天)";
}
