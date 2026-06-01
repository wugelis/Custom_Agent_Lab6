using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.UseCases.Rentals.Create;
using RentalCar.Domain.Exceptions;
using RentalCar.Domain.ValueObjects;
using RentalCar.Web.Models;

namespace RentalCar.Web.Controllers;

public class RentalController : Controller
{
    private readonly CreateRentalCommandHandler _createRentalHandler;

    public RentalController(CreateRentalCommandHandler createRentalHandler)
    {
        _createRentalHandler = createRentalHandler;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var accountIdStr = HttpContext.Session.GetString("AccountId");
        if (string.IsNullOrEmpty(accountIdStr))
        {
            return RedirectToAction("Login", "Account", new { returnUrl = "/Rental" });
        }

        var model = new CreateRentalViewModel
        {
            AvailableVehicleTypes = VehicleType.All.Select(v => new VehicleTypeOption
            {
                Name = v.Name,
                DailyRate = v.DailyRate
            }).ToList()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRentalViewModel model)
    {
        var accountIdStr = HttpContext.Session.GetString("AccountId");
        if (string.IsNullOrEmpty(accountIdStr))
        {
            return RedirectToAction("Login", "Account", new { returnUrl = "/Rental" });
        }

        if (!Guid.TryParse(accountIdStr, out var accountId))
        {
            return RedirectToAction("Login", "Account");
        }

        if (!ModelState.IsValid)
        {
            model.AvailableVehicleTypes = VehicleType.All.Select(v => new VehicleTypeOption
            {
                Name = v.Name,
                DailyRate = v.DailyRate
            }).ToList();
            return View("Index", model);
        }

        try
        {
            var command = new CreateRentalCommand(
                accountId,
                model.VehicleTypeName,
                model.StartDate,
                model.EndDate
            );

            var result = await _createRentalHandler.HandleAsync(command);

            TempData["Success"] = $"租用成功！租金總額：{result.TotalFee:N0} 元";
            TempData["RentalDetails"] = $"車型：{result.VehicleTypeName}｜期間：{result.Period}";

            return RedirectToAction(nameof(Success), new { id = result.RentalId });
        }
        catch (DomainException ex)
        {
            ModelState.AddModelError("", ex.Message);
            model.AvailableVehicleTypes = VehicleType.All.Select(v => new VehicleTypeOption
            {
                Name = v.Name,
                DailyRate = v.DailyRate
            }).ToList();
            return View("Index", model);
        }
    }

    [HttpGet]
    public IActionResult Success(Guid id)
    {
        ViewBag.RentalId = id;
        return View();
    }
}
