using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.UseCases.Accounts.Login;
using RentalCar.Application.UseCases.Accounts.Register;
using RentalCar.Domain.Exceptions;
using RentalCar.Web.Models;

namespace RentalCar.Web.Controllers;

public class AccountController : Controller
{
    private readonly RegisterAccountCommandHandler _registerHandler;
    private readonly LoginCommandHandler _loginHandler;

    public AccountController(
        RegisterAccountCommandHandler registerHandler,
        LoginCommandHandler loginHandler)
    {
        _registerHandler = registerHandler;
        _loginHandler = loginHandler;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View(new RegisterViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (model.Password != model.ConfirmPassword)
        {
            ModelState.AddModelError("ConfirmPassword", "密碼確認不一致");
            return View(model);
        }

        try
        {
            var command = new RegisterAccountCommand(
                model.Username,
                model.Password,
                model.Email
            );

            var accountId = await _registerHandler.HandleAsync(command);

            TempData["Success"] = "註冊成功！請登入。";
            return RedirectToAction(nameof(Login));
        }
        catch (DomainException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(model);
        }
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            var command = new LoginCommand(model.Username, model.Password);
            var result = await _loginHandler.HandleAsync(command);

            if (result.Success)
            {
                // 儲存登入狀態到 Session
                HttpContext.Session.SetString("AccountId", result.AccountId!.Value.ToString());
                HttpContext.Session.SetString("Username", model.Username);

                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);

                return RedirectToAction("Index", "Rental");
            }

            ModelState.AddModelError("", result.Message ?? "登入失敗");
            return View(model);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "登入過程發生錯誤");
            return View(model);
        }
    }

    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
