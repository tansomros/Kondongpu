using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Labs.Commands.Create;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Labs.Commands.Update;
public record UpdateCreateLabListCommand : IRequest<Unit>
{
    public required List<CreateLabCommand> UpdateCreateList { get; set; }
}

public class UpdateCreateLabListCommandHandler : IRequestHandler<UpdateCreateLabListCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public UpdateCreateLabListCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCreateLabListCommand request, CancellationToken cancellationToken)
    {
        var createLabList = new List<Lab>();
        foreach (var item in request.UpdateCreateList)
        {
            var checkupItem = await _context.CheckupItems.FirstAsync(i => i.LabItemCode == item.LabItemCode, cancellationToken);

            var lab = await _context.Labs.FirstOrDefaultAsync(
                l => l.CheckupId == item.CheckupId
                && l.VisitNumber == item.VisitNumber
                && l.CheckupItemId == checkupItem.Id
                , cancellationToken);

            if (lab != null) //Update
            {
                lab.ResultValue = item.ResultValue;
                lab.ReferenceRange = item.ReferenceRange;
                lab.IsAbnormal = item.IsAbnormal;
                lab.Comments = item.Comments;
                lab.ResultDate = item.ResultDate;
                lab.ResultTime = item.ResultTime;
                //lab.Lab_Item_Code = int.Parse(item.LabItemCode);
                //lab.Lab_Item_Name = item.LabItemName;

                _context.Labs.Update(lab);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else//Create
            {
                createLabList.Add( 
                    new Lab(
                    item.CheckupId,
                    item.VisitNumber,
                    checkupItem.Id)
                    {
                        Comments = item.Comments,
                        ResultDate = item.ResultDate,
                        IsAbnormal = item.IsAbnormal,
                        ReferenceRange = item.ReferenceRange,
                        ResultTime = item.ResultTime,
                        ResultValue = item.ResultValue,
                        Lab_Item_Code = int.Parse(item.LabItemCode),
                        Lab_Item_Name= item.LabItemName,
                    }
                );
            }
        }

        if (createLabList.Count != 0)
        {
            await _context.Labs.AddRangeAsync(createLabList, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}

public class UpdateCreateLabListCommandValidator : AbstractValidator<UpdateCreateLabListCommand>
{
    private string? _labItemCode;
    private readonly KondongpuDatabaseContext _context;
    public UpdateCreateLabListCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;

        RuleFor(p => p.UpdateCreateList).MustAsync(CheckupItemCodeExistAsync)
            .WithMessage($"ไม่พบรายการ LabItemCode หมายเลข {_labItemCode} ในตาราง CheckupItems ที่ท่านระบุ ");
    }

    public async Task<bool> CheckupItemCodeExistAsync(List<CreateLabCommand> updateCreateList, CancellationToken cancellationToken)
    {
        foreach (var item in updateCreateList)
        {
            _labItemCode = item.LabItemCode;
            var checkupItem = await _context.CheckupItems.AsNoTracking().FirstOrDefaultAsync(c => c.LabItemCode == _labItemCode, cancellationToken);
            if (checkupItem == null) { return false; }
        }
        return true;
    }
}
