namespace Kondongpu.Application.Features.Checkups.Commands.Update;
public class UpdateCheckupLabXrayOrder
{
    public required string VisitNumber { get; set; }

    public required List<string> LabOrder { get; set; }

    public required List<string> XrayOrder { get; set; }

    public required List<string> ServiceOrder { get; set; }
}
