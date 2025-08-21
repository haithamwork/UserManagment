using MediatR;
using TerminalMgmt.Domain.Entities;

namespace TerminalMgmt.Application.Terminals;

// Queries
public record GetTerminalBySerialQuery(string SerialNumber) : IRequest<Terminal?>;
public record ListTerminalsQuery() : IRequest<IReadOnlyList<Terminal>>;

// Commands
public record RegisterTerminalCommand(string SerialNumber, string Model, string FirmwareVersion, Guid ProfileId) : IRequest<Terminal>;
public record UpdateTerminalProfileCommand(Guid TerminalId, Guid ProfileId) : IRequest;
public record IngestStatisticCommand(Guid TerminalId, string Metric, double Value) : IRequest;
