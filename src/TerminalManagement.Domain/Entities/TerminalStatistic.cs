using System;

namespace TerminalManagement.Domain.Entities
{
    public class TerminalStatistic
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TerminalProfileId { get; set; }
        public TerminalProfile? TerminalProfile { get; set; }

        public DateTime TimestampUtc { get; set; } = DateTime.UtcNow;
        public string Metric { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}

