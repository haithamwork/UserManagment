using System;

namespace TerminalManagement.Domain.Entities
{
    public class ApnSetting
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TerminalProfileId { get; set; }
        public TerminalProfile? TerminalProfile { get; set; }

        public string Apn { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

