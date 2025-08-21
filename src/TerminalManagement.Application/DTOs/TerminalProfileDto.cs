using System;
using System.Collections.Generic;

namespace TerminalManagement.Application.DTOs
{
    public class TerminalProfileDto
    {
        public Guid Id { get; set; }
        public string TerminalId { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string FirmwareVersion { get; set; } = string.Empty;
        public Guid IssuerId { get; set; }
        public List<ApnSettingDto> ApnSettings { get; set; } = new();
    }
}

