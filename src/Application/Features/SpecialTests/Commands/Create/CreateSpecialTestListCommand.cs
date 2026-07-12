using System.Collections;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.SpecialTests.Commands.Create;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.SpecialTests.Commands.Update;
public class CreateSpecialTestListCommand : IRequest<Unit>
{
    public required List<CreateSpecialTestCommand> SpecialTestList { get; set; }
}

public class CreateSpecialTestListCommandHandler : IRequestHandler<CreateSpecialTestListCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public CreateSpecialTestListCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CreateSpecialTestListCommand request, CancellationToken cancellationToken)
    {
        var spItemList = new List<SpecialTest>();
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
                //specialTest.IsAbnormal = item.IsAbnormal;
                specialTest.ResultReport = item.ResultReport;

                _context.SpecialTests.Update(specialTest);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                spItemList.Add(
                    new SpecialTest(
                                item.CheckupId,
                                item.VisitNumber,
                                item.CheckupItemId                        
                                )
                    {
                        ResultValue = item.ResultValue,
                        ReferenceRange = item.ReferenceRange,
                        IsAbnormal = item.IsAbnormal,
                        ResultReport = item.ResultReport,
                        Comments = item.Comments,
                        ResultDate = item.ResultDate,
                        ResultTime = item.ResultTime
                    });
            }
        }

        if (spItemList.Count != 0)
        {
            await _context.SpecialTests.AddRangeAsync(spItemList, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}

public class CreateSpecialTestListCommandValidator : AbstractValidator<CreateSpecialTestListCommand>
{
    private int? _labItemId;
    private readonly KondongpuDatabaseContext _context;
    public CreateSpecialTestListCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;

        RuleFor(p => p.SpecialTestList).MustAsync(CheckupItemIdExistAsync)
            .WithMessage($"ไม่พบรายการ CheckupItems หมายเลข Id: {_labItemId} ในตาราง CheckupItems ที่ท่านระบุ ");
    }
    public async Task<bool> CheckupItemIdExistAsync(List<CreateSpecialTestCommand> updateCreateList, CancellationToken cancellationToken)
    {
        foreach (var item in updateCreateList)
        {
            _labItemId = item.CheckupItemId;
            var checkupItem = await _context.CheckupItems.AsNoTracking().FirstOrDefaultAsync(c => c.Id == _labItemId, cancellationToken);
            if (checkupItem == null) { return false; }
        }
        return true;
    }
}
