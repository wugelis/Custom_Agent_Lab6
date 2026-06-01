using RentalCar.Domain.ValueObjects;

namespace RentalCar.Domain.Entities;

/// <summary>
/// 車輛實體 - 負責車輛資訊管理
/// </summary>
public sealed class Vehicle
{
    public Guid VehicleId { get; private set; }
    public VehicleType VehicleType { get; private set; }
    public string LicensePlate { get; private set; }
    public bool IsAvailable { get; private set; }

    private Vehicle(Guid vehicleId, VehicleType vehicleType, string licensePlate)
    {
        VehicleId = vehicleId;
        VehicleType = vehicleType;
        LicensePlate = licensePlate;
        IsAvailable = true;
    }

    public static Vehicle Create(VehicleType vehicleType, string licensePlate) =>
        new(Guid.NewGuid(), vehicleType, licensePlate);

    public void SelectVehicleType(VehicleType vehicleType)
    {
        VehicleType = vehicleType;
    }

    public void VehicleTypeConfirmed()
    {
        // 確認車型選擇的商業邏輯
    }

    public void MarkAsUnavailable() => IsAvailable = false;
    public void MarkAsAvailable() => IsAvailable = true;
}
