namespace Tms.Domain.Entities;

public class ApplicationPackage
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = string.Empty;
	public string Version { get; set; } = "1.0.0";
	public string FileName { get; set; } = string.Empty;
	public long FileSizeBytes { get; set; }
	public string ContentType { get; set; } = "application/octet-stream";
	public string Sha256 { get; set; } = string.Empty;
	public bool IsActive { get; set; } = true;
	public DateTimeOffset UploadedAtUtc { get; set; } = DateTimeOffset.UtcNow;
}