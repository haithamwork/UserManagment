namespace Tms.Domain.Entities;

public class ConfigurationProfile
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; }
	public bool IsDefault { get; set; }
	public DateTimeOffset CreatedAtUtc { get; set; } = DateTimeOffset.UtcNow;

	// PSS settings
	public string PssHost { get; set; } = string.Empty;
	public int PssPort { get; set; }
	public bool UseTls { get; set; }
	public int ConnectionTimeoutSeconds { get; set; } = 30;
	public string? TerminalIdOnSwitch { get; set; }
	public string? MerchantIdOnSwitch { get; set; }

	// Network settings
	public int TcpKeepAliveSeconds { get; set; } = 60;

	// Minimal EMV/Payment settings
	public string? CurrencyCodeNumeric { get; set; }
	public string? CountryCodeNumeric { get; set; }
}