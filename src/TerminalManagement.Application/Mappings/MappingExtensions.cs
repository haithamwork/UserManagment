using System.Linq;
using TerminalManagement.Application.DTOs;
using TerminalManagement.Domain.Entities;

namespace TerminalManagement.Application.Mappings
{
    public static class MappingExtensions
    {
        public static TerminalProfileDto ToDto(this TerminalProfile entity)
        {
            return new TerminalProfileDto
            {
                Id = entity.Id,
                TerminalId = entity.TerminalId,
                Model = entity.Model,
                FirmwareVersion = entity.FirmwareVersion,
                IssuerId = entity.IssuerId,
                ApnSettings = entity.ApnSettings.Select(a => new ApnSettingDto
                {
                    Id = a.Id,
                    Apn = a.Apn,
                    Username = a.Username,
                    Password = a.Password
                }).ToList()
            };
        }

        public static TerminalConfigDto ToConfigDto(this TerminalProfile entity)
        {
            return new TerminalConfigDto
            {
                TerminalId = entity.TerminalId,
                FirmwareVersion = entity.FirmwareVersion,
                ApnSettings = entity.ApnSettings.Select(a => new ApnSettingDto
                {
                    Id = a.Id,
                    Apn = a.Apn,
                    Username = a.Username,
                    Password = a.Password
                }).ToList(),
                DownloadUrls = { }
            };
        }
    }
}

