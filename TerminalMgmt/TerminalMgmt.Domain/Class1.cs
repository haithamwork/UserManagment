namespace TerminalMgmt.Domain.Entities;

public class Terminal
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public string SerialNumber { get; set; } = string.Empty;
	public string Model { get; set; } = string.Empty;
	public string FirmwareVersion { get; set; } = string.Empty;
	public Guid TerminalProfileId { get; set; }
	public TerminalProfile? Profile { get; set; }
	public DateTime LastSeenUtc { get; set; }
}

public class TerminalProfile
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = string.Empty;
	public string Apns { get; set; } = string.Empty; // Application parameters namespace string (json)
	public string Issuer { get; set; } = string.Empty; // e.g., acquirer/issuer code
	public string ConfigJson { get; set; } = "{}";
	public ICollection<Terminal> Terminals { get; set; } = new List<Terminal>();
}

public class TerminalStatistic
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public Guid TerminalId { get; set; }
	public Terminal? Terminal { get; set; }
	public DateTime TimestampUtc { get; set; } = DateTime.UtcNow;
	public string Metric { get; set; } = string.Empty;
	public double Value { get; set; }
}
