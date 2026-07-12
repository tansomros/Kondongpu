using System.Collections;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.SpecialTests.Commands.Create;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.SpecialTests.Commands.Update;
public class UpdateCreateSpecialTestListCommand : IRequest<Unit>
{
    public required List<CreateSpecialTestCommand> SpecialTestList { get; set; }
}

public class UpdateCreateSpecialTestListCommandHandler : IRequestHandler<UpdateCreateSpecialTestListCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public UpdateCreateSpecialTestListCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCreateSpecialTestListCommand request, CancellationToken cancellationToken)
    {
        var SpecialTestItemList = new List<SpecialTest>();
        foreach (var item in request.SpecialTestList)
        {
            var SpecialTest = await _context.SpecialTests.FirstOrDefaultAsync(
                l => l.CheckupId == item.CheckupId
                && l.VisitNumber == item.VisitNumber
                && l.CheckupItemId == item.CheckupItemId
                , cancellationToken
                );

            if ( SpecialTest != null )
            {
                SpecialTest.ResultValue = item.ResultValue;
                SpecialTest.IsAbnormal = item.IsAbnormal;
                SpecialTest.ResultReport = item.ResultReport;

                _context.SpecialTests.Update(SpecialTest);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                SpecialTestItemList.Add(
                    new SpecialTest(
                                item.CheckupId,
                                item.VisitNumber,
                                item.CheckupItemId                        
                                )
                    {
                        IsAbnormal = item.IsAbnormal,
                        ResultReport = item.ResultReport,
                        ResultValue = item.ResultValue
                    });
            }
        }

        if (SpecialTestItemList.Count != 0)
        {
            await _context.SpecialTests.AddRangeAsync(SpecialTestItemList, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}

public class UpdateCreateSpecialTestListCommandValidator : AbstractValidator<UpdateCreateSpecialTestListCommand>
{
    private int? _SpecialTestItemId;
    private readonly KondongpuDatabaseContext _context;
    public UpdateCreateSpecialTestListCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;

        RuleFor(p => p.SpecialTestList).MustAsync(CheckupItemIdExistAsync)
            .WithMessage($"ไม่พบรายการ CheckupItems หมายเลข Id: {_SpecialTestItemId} ในตาราง CheckupItems ที่ท่านระบุ ");
    }
    public async Task<bool> CheckupItemIdExistAsync(List<CreateSpecialTestCommand> updateCreateList, CancellationToken cancellationToken)
    {
        foreach (var item in updateCreateList)
        {
            _SpecialTestItemId = item.CheckupItemId;
            var checkupItem = await _context.CheckupItems.AsNoTracking().FirstOrDefaultAsync(c => c.Id == _SpecialTestItemId, cancellationToken);
            if (checkupItem == null) { return false; }
        }
        return true;
    }
}
