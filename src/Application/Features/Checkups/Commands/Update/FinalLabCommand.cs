namespace Kondongpu.Application.Features.Checkups.Commands.Update;
public class FinalLabCommand
{
    public required string CheckupGroupCode { get; set; }
    public required string Name { get; set; }
    public string? ResultValue { get; set; }
    public required string ReferenceRange { get; set; }
    public string? IsAbnormal { get; set; }
}
