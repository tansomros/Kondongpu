namespace Kondongpu.Application.Features.Checkups.Commands.Update;
public class UpsertCheckupLabXrayIsResultReadyCommand
{
    public required string VisitNumber { get; set; }
    public required bool IsLabResultReady { get; set; }
    public required bool IsXrayResultReady { get; set; }
}
