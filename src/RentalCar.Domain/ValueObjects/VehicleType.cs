namespace RentalCar.Domain.ValueObjects;

/// <summary>
/// 車輛類型值物件
/// </summary>
public sealed record VehicleType
{
    public string Name { get; }
    public decimal DailyRate { get; }

    private VehicleType(string name, decimal dailyRate)
    {
        Name = name;
        DailyRate = dailyRate;
    }

    public static readonly VehicleType Car = new("轎車", 1000m);
    public static readonly VehicleType SUV = new("休旅車", 1500m);
    public static readonly VehicleType Truck = new("貨車", 2000m);
    public static readonly VehicleType SportsCar = new("跑車", 3000m);
    public static readonly VehicleType ElectricCar = new("電動車", 2800m);

    public static IReadOnlyList<VehicleType> All => new[]
    {
        Car, SUV, Truck, SportsCar, ElectricCar
    };

    public static VehicleType? FromName(string name) =>
        All.FirstOrDefault(v => v.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
}
