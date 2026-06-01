namespace RentalCar.Domain.ValueObjects;

/// <summary>
/// 金額值物件
/// </summary>
public sealed record Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    private Money(decimal amount, string currency = "TWD")
    {
        if (amount < 0)
            throw new ArgumentException("金額不可為負值", nameof(amount));

        Amount = amount;
        Currency = currency;
    }

    public static Money FromDecimal(decimal amount, string currency = "TWD") =>
        new(amount, currency);

    public static Money Zero => new(0m);

    public override string ToString() => $"{Amount:N0} {Currency}";
}
