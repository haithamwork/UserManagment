using MediatR;
using Microsoft.EntityFrameworkCore;
using TerminalMgmt.Application.Terminals;
using TerminalMgmt.Infrastructure.Db;
using TerminalMgmt.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ListTerminalsQuery).Assembly));

// EF Core Sqlite
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlite("Data Source=terminalmgmt.db"));

// Repositories
builder.Services.AddScoped<ITerminalRepository, TerminalRepository>();
builder.Services.AddScoped<ITerminalProfileRepository, TerminalProfileRepository>();
builder.Services.AddScoped<IStatisticsRepository, StatisticsRepository>();

// SignalR
builder.Services.AddSignalR().AddJsonProtocol();

var app = builder.Build();

// Apply migrations / create database (demo-purpose)
using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<TerminalMgmt.Web.Hubs.TerminalHub>("/terminalHub");

app.Run();
