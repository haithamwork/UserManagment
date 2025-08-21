using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TerminalMgmt.Domain.Entities;
using TerminalMgmt.Infrastructure.Db;

namespace TerminalMgmt.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfilesController : ControllerBase
{
	private readonly AppDbContext _db;
	public ProfilesController(AppDbContext db) => _db = db;

	[HttpGet]
	public async Task<IActionResult> List()
		=> Ok(await _db.TerminalProfiles.AsNoTracking().ToListAsync());

	public record UpsertDto(string Name, string Apns, string Issuer, string ConfigJson);

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] UpsertDto dto)
	{
		var p = new TerminalProfile { Name = dto.Name, Apns = dto.Apns, Issuer = dto.Issuer, ConfigJson = dto.ConfigJson };
		_db.TerminalProfiles.Add(p);
		await _db.SaveChangesAsync();
		return Ok(new { p.Id });
	}
}

