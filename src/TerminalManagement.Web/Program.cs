using MediatR;
using TerminalManagement.Infrastructure;
using TerminalManagement.Web.Sockets;
using TerminalManagement.Web.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(TerminalManagement.Application.Terminals.Queries.ListTerminalProfilesQuery).Assembly));
builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.MapHub<TerminalHub>("/terminalHub");

await DataSeeder.SeedAsync(app.Services);

app.Run();