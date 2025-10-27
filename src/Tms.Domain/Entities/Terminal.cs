namespace Tms.Domain.Entities;

public class Terminal
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public string SerialNumber { get; set; } = string.Empty;
	public string? Model { get; set; }
	public string? MerchantCode { get; set; }
	public DateTimeOffset CreatedAtUtc { get; set; } = DateTimeOffset.UtcNow;
	public DateTimeOffset? LastSeenAtUtc { get; set; }
	public Guid? AssignedProfileId { get; set; }
	public Guid? AssignedAppPackageId { get; set; }
}