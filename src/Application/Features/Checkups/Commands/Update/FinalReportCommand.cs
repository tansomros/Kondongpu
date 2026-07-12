namespace Kondongpu.Application.Features.Checkups.Commands.Update;
public class FinalReportCommand
{
    public ICollection<FinalLabCommand> Labs { get; set; }

    public FinalVisionCommand? Vision { get; set; }

    public FinalReportCommand()
    {
        Labs = [];
    }
}
