using RentalCar.Application.Abstractions.Repositories;
using RentalCar.Application.UseCases.Accounts.Login;
using RentalCar.Application.UseCases.Accounts.Register;
using RentalCar.Application.UseCases.Rentals.Create;
using RentalCar.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// 註冊 Repositories（Ports → Adapters 綁定）
builder.Services.AddSingleton<IAccountRepository, InMemoryAccountRepository>();
builder.Services.AddSingleton<IRentalRepository, InMemoryRentalRepository>();

// 註冊 Command Handlers
builder.Services.AddScoped<RegisterAccountCommandHandler>();
builder.Services.AddScoped<LoginCommandHandler>();
builder.Services.AddScoped<CreateRentalCommandHandler>();

// 配置 Session（用於登入狀態）
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
