using System.Collections;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.Xrays.Commands.Create;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Xrays.Commands.Update;
public class UpdateXrayListCommand : IRequest<Unit>
{
    public required List<UpdateXrayCommand> XrayList { get; set; }
}

public class UpdateXrayListCommandHandler : IRequestHandler<UpdateXrayListCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public UpdateXrayListCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateXrayListCommand request, CancellationToken cancellationToken)
    {
        var xrayItemList = new List<Xray>();
        foreach (var item in request.XrayList)
        {
            var xray = await _context.Xrays.FirstOrDefaultAsync(
                l => l.Id == item.Id
                //&& l.VisitNumber == item.VisitNumber
                //&& l.CheckupItemId == item.CheckupItemId
                , cancellationToken
                );

            if ( xray != null )
            {
                xray.ResultValue = item.ResultValue;
                xray.IsAbnormal = item.IsAbnormal;
                xray.ResultReport = item.ResultReport;

                _context.Xrays.Update(xray);
                await _context.SaveChangesAsync(cancellationToken);
            }
            //else
            //{
            //    xrayItemList.Add(
            //        new Xray(
            //                    item.CheckupId,
            //                    item.VisitNumber,
            //                    item.CheckupItemId,
            //                    item.ResultValue,
            //                    item.ReportText,
            //                    item.AccessionNumber
            //                    )
            //        {
            //            IsAbnormal = item.IsAbnormal,
            //            ResultReport = item.ResultReport,
            //        });
            //}
        }

        if (xrayItemList.Count != 0)
        {
            await _context.Xrays.AddRangeAsync(xrayItemList, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}

public class UpdateXrayListCommandValidator : AbstractValidator<UpdateXrayListCommand>
{
    private int? _labItemId;
    private readonly KondongpuDatabaseContext _context;
    public UpdateXrayListCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;

        RuleFor(p => p.XrayList).MustAsync(CheckupItemIdExistAsync)
            .WithMessage($"ไม่พบรายการ CheckupItems หมายเลข Id: {_labItemId} ในตาราง CheckupItems ที่ท่านระบุ ");
    }
    public async Task<bool> CheckupItemIdExistAsync(List<UpdateXrayCommand> updateCreateList, CancellationToken cancellationToken)
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
