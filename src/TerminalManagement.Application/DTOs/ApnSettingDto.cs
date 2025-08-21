using System;

namespace TerminalManagement.Application.DTOs
{
    public class ApnSettingDto
    {
        public Guid Id { get; set; }
        public string Apn { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

