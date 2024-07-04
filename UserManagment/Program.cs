using Microsoft.EntityFrameworkCore;
using UserManagment.Infrastructure.DataBase;
using UserManagment.Infrastructure;
using UserManagment.Service;
using UserManagment.Application;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
#region Connecting To Sql Server

builder.Services.AddDbContext<ApplicationDbContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("MainString"));
});

#endregion
#region Dependency Injections
builder.Services.AddInfrastructureDependencies().AddServiceDependencies().AddApplicationDependencies();

#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}");
app.MapControllers();



app.Run();
