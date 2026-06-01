namespace RentalCar.Web.Models;

public sealed class CreateRentalViewModel
{
    public string VehicleTypeName { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
    
    public List<VehicleTypeOption> AvailableVehicleTypes { get; set; } = new();
}

public sealed class VehicleTypeOption
{
    public string Name { get; set; } = string.Empty;
    public decimal DailyRate { get; set; }
}
