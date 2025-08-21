using System;
using System.Collections.Generic;

namespace TerminalManagement.Domain.Entities
{
    public class TerminalProfile
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TerminalId { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string FirmwareVersion { get; set; } = string.Empty;
        public Guid IssuerId { get; set; }
        public Issuer? Issuer { get; set; }

        public ICollection<ApnSetting> ApnSettings { get; set; } = new List<ApnSetting>();
        public ICollection<TerminalStatistic> Statistics { get; set; } = new List<TerminalStatistic>();
    }
}

