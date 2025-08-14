var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Infrastructure registrations
builder.Services.AddDbContext<Tms.Infrastructure.TmsDbContext>(options =>
{
	var cs = builder.Configuration.GetConnectionString("Default") ?? "Data Source=tms.db";
	options.UseSqlite(cs);
});

builder.Services.AddScoped<Tms.Application.Abstractions.ITerminalService, Tms.Infrastructure.Services.TerminalService>();
builder.Services.AddScoped<Tms.Application.Abstractions.IConfigurationProfileService, Tms.Infrastructure.Services.ConfigurationProfileService>();
builder.Services.AddScoped<Tms.Application.Abstractions.IApplicationPackageService, Tms.Infrastructure.Services.ApplicationPackageService>();

var app = builder.Build();

// Ensure database created/migrated
using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<Tms.Infrastructure.TmsDbContext>();
	await db.Database.EnsureCreatedAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// Terminals
app.MapPost("/api/terminals/register", async (string serialNumber, string? model, string? merchantCode, Tms.Application.Abstractions.ITerminalService svc, CancellationToken ct) =>
{
	var t = await svc.RegisterAsync(serialNumber, model, merchantCode, ct);
	return Results.Ok(t);
}).WithOpenApi();

app.MapGet("/api/terminals/by-serial/{serial}", async (string serial, Tms.Application.Abstractions.ITerminalService svc, CancellationToken ct) =>
{
	var t = await svc.GetBySerialAsync(serial, ct);
	return t is not null ? Results.Ok(t) : Results.NotFound();
}).WithOpenApi();

app.MapPost("/api/terminals/{id:guid}/assign/profile/{profileId:guid}", async (Guid id, Guid profileId, Tms.Application.Abstractions.ITerminalService svc, CancellationToken ct) =>
{
	await svc.AssignProfileAsync(id, profileId, ct);
	return Results.NoContent();
}).WithOpenApi();

app.MapPost("/api/terminals/{id:guid}/assign/app/{appId:guid}", async (Guid id, Guid appId, Tms.Application.Abstractions.ITerminalService svc, CancellationToken ct) =>
{
	await svc.AssignApplicationAsync(id, appId, ct);
	return Results.NoContent();
}).WithOpenApi();

// Configuration Profiles
app.MapPost("/api/profiles", async (Tms.Domain.Entities.ConfigurationProfile profile, Tms.Application.Abstractions.IConfigurationProfileService svc, CancellationToken ct) =>
{
	var created = await svc.CreateAsync(profile, ct);
	return Results.Created($"/api/profiles/{created.Id}", created);
}).WithOpenApi();

app.MapGet("/api/profiles", async (Tms.Application.Abstractions.IConfigurationProfileService svc, CancellationToken ct) =>
{
	var list = await svc.ListAsync(ct);
	return Results.Ok(list);
}).WithOpenApi();

app.MapGet("/api/profiles/{id:guid}", async (Guid id, Tms.Application.Abstractions.IConfigurationProfileService svc, CancellationToken ct) =>
{
	var p = await svc.GetAsync(id, ct);
	return p is not null ? Results.Ok(p) : Results.NotFound();
}).WithOpenApi();

// Application Packages
app.MapPost("/api/apps", async (HttpRequest request, Tms.Application.Abstractions.IApplicationPackageService svc, CancellationToken ct) =>
{
	if (!request.HasFormContentType) return Results.BadRequest("multipart/form-data expected");
	var form = await request.ReadFormAsync(ct);
	var file = form.Files["file"]; // file input name
	if (file is null || file.Length == 0) return Results.BadRequest("file missing");
	var name = form["name"].FirstOrDefault() ?? file.FileName;
	var version = form["version"].FirstOrDefault() ?? "1.0.0";
	var pkg = new Tms.Domain.Entities.ApplicationPackage
	{
		Name = name,
		Version = version,
		FileName = file.FileName,
		ContentType = file.ContentType
	};
	await using var stream = file.OpenReadStream();
	var created = await svc.CreateAsync(pkg, stream, ct);
	return Results.Created($"/api/apps/{created.Id}", created);
}).DisableAntiforgery().WithOpenApi();

app.MapGet("/api/apps", async (Tms.Application.Abstractions.IApplicationPackageService svc, CancellationToken ct) =>
{
	var list = await svc.ListAsync(ct);
	return Results.Ok(list);
}).WithOpenApi();

app.MapGet("/api/apps/{id:guid}/download", async (Guid id, Tms.Application.Abstractions.IApplicationPackageService svc, CancellationToken ct) =>
{
	var res = await svc.GetContentAsync(id, ct);
	return res is null ? Results.NotFound() : Results.File(res.Value.Content, res.Value.ContentType, res.Value.FileName);
}).WithOpenApi();

app.Run();

// Note: removed weather sample
