using System.Collections;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.SpecialTests.Commands.Create;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.SpecialTests.Commands.Update;
public class UpdateSpecialTestListCommand : IRequest<Unit>
{
    public required List<CreateSpecialTestCommand> SpecialTestList { get; set; }
}

public class UpdateSpecialTestListCommandHandler : IRequestHandler<UpdateSpecialTestListCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public UpdateSpecialTestListCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateSpecialTestListCommand request, CancellationToken cancellationToken)
    {
        var specialItemList = new List<SpecialTest>();
        foreach (var item in request.SpecialTestList)
        {
            var specialTest = await _context.SpecialTests.FirstOrDefaultAsync(
                l => l.CheckupId == item.CheckupId
                && l.VisitNumber == item.VisitNumber
                && l.CheckupItemId == item.CheckupItemId
                , cancellationToken
                );

            if ( specialTest != null )
            {
                specialTest.ResultValue = item.ResultValue;
                specialTest.IsAbnormal = item.IsAbnormal;
                specialTest.ResultReport = item.ResultReport;

                _context.SpecialTests.Update(specialTest);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                specialItemList.Add(
                    new SpecialTest(
                                item.CheckupId,
                                item.VisitNumber,
                                item.CheckupItemId
                                )
                    {
                        ResultValue = item.ResultValue,                        
                        IsAbnormal = item.IsAbnormal,
                        ResultReport = item.ResultReport,
                    });
            }
        }

        if (specialItemList.Count != 0)
        {
            await _context.SpecialTests.AddRangeAsync(specialItemList, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}

public class UpdateSpecialTestListCommandValidator : AbstractValidator<UpdateSpecialTestListCommand>
{
    private int? _specialTestId;
    private readonly KondongpuDatabaseContext _context;
    public UpdateSpecialTestListCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;

        RuleFor(p => p.SpecialTestList).MustAsync(CheckupItemIdExistAsync)
            .WithMessage($"ไม่พบรายการ CheckupItems หมายเลข Id: {_specialTestId} ในตาราง CheckupItems ที่ท่านระบุ ");
    }
    public async Task<bool> CheckupItemIdExistAsync(List<CreateSpecialTestCommand> updateCreateList, CancellationToken cancellationToken)
    {
        foreach (var item in updateCreateList)
        {
            _specialTestId = item.CheckupItemId;
            var checkupItem = await _context.CheckupItems.AsNoTracking().FirstOrDefaultAsync(c => c.Id == _specialTestId, cancellationToken);
            if (checkupItem == null) { return false; }
        }
        return true;
    }
}
