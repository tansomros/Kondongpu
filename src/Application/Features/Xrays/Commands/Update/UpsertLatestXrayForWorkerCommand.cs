
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Xrays.Commands.Update;
public class UpsertLatestXrayForWorkerCommand : IRequest<Unit>
{
    public required List<UpsertXrayCommand> UpsertXrayCommands { get; set; }
}

public class UpsertXrayCommand
{
    public required string VisitNumber { get; set; }
    public required string AccessionNumber { get; set; }
    public int HosxpItemCode { get; set; }
    public string? ResultValue { get; set; }
    public string? IsAbnormal { get; set; }
    public string? ResultReport { get; set; }
    public required string ReportText { get; set; }
}

public class UpsertLatestXrayForWorkerCommandHandler(KondongpuDatabaseContext context) : IRequestHandler<UpsertLatestXrayForWorkerCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context = context;

    public async Task<Unit> Handle(UpsertLatestXrayForWorkerCommand request, CancellationToken cancellationToken)
    {
        var xrayList = new List<Xray>();
        var removeDuplicateUpsertList = request.UpsertXrayCommands
            .GroupBy(x => new
            {
                x.VisitNumber,
                x.AccessionNumber,
                x.HosxpItemCode,
                //x.ResultValue,
                //x.IsAbnormal,
                //x.ResultReport,
                x.ReportText
            })
            .Select(g => g.First())
            .ToList();
        foreach (var item in removeDuplicateUpsertList)
        {
            var checkupItem = await _context.CheckupItems.FirstOrDefaultAsync(
                i => i.LabItemCode == item.HosxpItemCode.ToString(), cancellationToken
                );
            var checkupItemId = checkupItem?.Id ?? 0;

            var xray = await _context.Xrays.FirstOrDefaultAsync(
                l => l.VisitNumber == item.VisitNumber
                //&& l.AccessionNumber == item.AccessionNumber
                && l.CheckupItemId == checkupItemId
                , cancellationToken);

            var checkup = await _context.Checkups.FirstOrDefaultAsync(
                c => c.VisitNumber == item.VisitNumber, cancellationToken
                );
            if ( checkup != null && checkupItem != null)
            {
                if(xray != null)
                {
                    //xray.ResultValue = item.ResultValue;
                    xray.IsAbnormal = item.IsAbnormal;
                    //xray.ResultReport = item.ResultReport;
                    xray.ReportText = item.ReportText;

                    _context.Xrays.Update(xray);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    xrayList.Add(new Xray(
                        checkup.Id, 
                        item.VisitNumber, 
                        checkupItemId, 
                        item.ResultValue, 
                        item.ReportText, 
                        item.AccessionNumber) 
                    { 
                        ResultReport = item.ResultReport,
                        IsAbnormal = item.IsAbnormal,
                    });
                }
            }
        }

        if (xrayList.Count != 0)
        {
            await _context.Xrays.AddRangeAsync(xrayList, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        //ตรวจสอบผล Xray หากออกครบ ให้ update status Ready
        var visitNumbers = removeDuplicateUpsertList.Select(u => u.VisitNumber).Distinct().ToList();
        foreach (var vn in visitNumbers)
        {
            var checkup = await _context.Checkups.FirstOrDefaultAsync(c => c.VisitNumber == vn, cancellationToken);
            if (checkup != null && checkup.XrayOrder != null && checkup.XrayOrder.Count != 0)
            {                
                //
                //IsXrayResultReady
                //
                var xrays = await _context.Xrays.AsNoTracking()
                    .Include(s => s.CheckupItem)
                    .Where(l => l.VisitNumber == vn)
                    .ToListAsync(cancellationToken);
                bool allXrays = checkup.XrayOrder.All(
                        order => xrays.Any(x => x.CheckupItem.LabItemCode?.ToString() == order)
                    );
                if (allXrays && !checkup.IsXrayResultReady)
                {
                    checkup.IsXrayResultReady = allXrays;
                }

                _context.Checkups.Update(checkup);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        return Unit.Value;
    }
}
