using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Labs.Commands.Create;

public record CreateLabCommand : IRequest<int>
{
    public required int CheckupId { get; set; }
    public required string VisitNumber { get; set; }
    public required string LabItemCode { get; set; }
    public string? LabItemName { get; set; }
    public string? ResultValue { get; set; }
    public string? ReferenceRange { get; set; }
    public string? IsAbnormal { get; set; }
    public string? Comments { get; set; }
    public DateOnly? ResultDate { get; set; }
    public TimeOnly ResultTime { get; set; }
}

public class CreateLabCommandHandler : IRequestHandler<CreateLabCommand, int>
{
    private readonly KondongpuDatabaseContext _context;

    public CreateLabCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateLabCommand request, CancellationToken cancellationToken)
    {
        var checkupItem = await _context.CheckupItems.FirstAsync(i => i.LabItemCode ==  request.LabItemCode, cancellationToken);

        var lab = new Lab(
            request.CheckupId,
            request.VisitNumber,
            checkupItem.Id)
        {
            Comments = request.Comments,
            ResultDate = request.ResultDate,
            IsAbnormal = request.IsAbnormal,
            ReferenceRange = request.ReferenceRange,
            ResultTime = request.ResultTime,
            ResultValue = request.ResultValue,
            Lab_Item_Name = request.LabItemName,
        };

        await _context.Labs.AddAsync(lab, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return lab.Id;
    }
}
