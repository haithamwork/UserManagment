using System.Collections.Generic;

namespace TerminalManagement.Application.DTOs
{
    public class TerminalConfigDto
    {
        public string TerminalId { get; set; } = string.Empty;
        public string FirmwareVersion { get; set; } = string.Empty;
        public List<ApnSettingDto> ApnSettings { get; set; } = new();
        public List<string> DownloadUrls { get; set; } = new();
    }
}

