namespace Kondongpu.Application.Features.Labs.Commands.Update;
public class UpsertLabCommand
{
    public required string VisitNumber { get; set; }
    public required int LabItemCode { get; set; }
    public string? LabItemName { get; set; }
    public string? ResultValue { get; set; }
    public string? ReferenceRange { get; set; }
    public string? IsAbnormal { get; set; }
    public DateOnly? ResultDate { get; set; }
    public TimeOnly ResultTime { get; set; }
}
