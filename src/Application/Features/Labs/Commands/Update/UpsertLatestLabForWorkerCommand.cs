using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Labs.Commands.Update;

public record UpsertLatestLabForWorkerCommand : IRequest<Unit>
{
    public required List<UpsertLabCommand> UpsertList { get; set; }
}

public class UpsertLatestLabForWorkerCommandHandler(KondongpuDatabaseContext context) : IRequestHandler<UpsertLatestLabForWorkerCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context = context;

    public async Task<Unit> Handle(UpsertLatestLabForWorkerCommand request, CancellationToken cancellationToken)
    {
        var upsertLabList = new List<Lab>();
        var removeDuplicateUpsertList = request.UpsertList
            .GroupBy(x => new
            {
                x.VisitNumber,
                x.LabItemName,
                x.ResultValue, 
                x.ReferenceRange, 
                x.IsAbnormal, 
                x.ResultDate, 
                x.ResultTime 
            })
            .Select(g => g.First())
            .ToList();
        foreach (var item in removeDuplicateUpsertList)
        {
            var checkupItem = await _context.CheckupItems.FirstOrDefaultAsync(
                i => i.LabItemCode == item.LabItemCode.ToString(), cancellationToken
                );
            var checkupItemId = checkupItem?.Id;

            var lab = await _context.Labs.FirstOrDefaultAsync(
                l => l.VisitNumber == item.VisitNumber
                && l.Lab_Item_Code == item.LabItemCode
                , cancellationToken);

            var checkup = await _context.Checkups.FirstAsync(c => c.VisitNumber == item.VisitNumber, cancellationToken);

            if ( lab != null )
            {
                lab.ResultValue = item.ResultValue;
                lab.ReferenceRange = item.ReferenceRange;
                lab.IsAbnormal = item.IsAbnormal;
                lab.ResultDate = item.ResultDate;
                lab.ResultTime = item.ResultTime;
                lab.Lab_Item_Code = item.LabItemCode;
                lab.Lab_Item_Name = item.LabItemName;

                _context.Labs.Update(lab);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                upsertLabList.Add(
                    new Lab(
                    checkup.Id,
                    item.VisitNumber,
                    checkupItemId)
                    {
                        ResultDate = item.ResultDate,
                        IsAbnormal = item.IsAbnormal,
                        ReferenceRange = item.ReferenceRange,
                        ResultTime = item.ResultTime,
                        ResultValue = item.ResultValue,
                        Lab_Item_Code= item.LabItemCode,
                        Lab_Item_Name= item.LabItemName,
                    }
                );
            }
        }

        if (upsertLabList.Count != 0)
        {
            await _context.Labs.AddRangeAsync(upsertLabList, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        //ตรวจสอบผล Lab หากออกครบ ให้ update status Ready
        var visitNumbers = removeDuplicateUpsertList.Select(u => u.VisitNumber).Distinct().ToList();
        foreach (var vn in visitNumbers)
        {
            var checkup = await _context.Checkups.FirstOrDefaultAsync(c => c.VisitNumber == vn, cancellationToken);
            if (checkup != null && checkup.LabOrder != null && checkup.LabOrder.Count != 0)
            {
                //
                //IsLabResultReady 
                //
                var labs = await _context.Labs.AsNoTracking().Where(l => l.VisitNumber == vn).ToListAsync(cancellationToken);                
                // Compare Labs against LabOrder
                bool allAccountedFor = checkup.LabOrder.All(
                    order => labs.Any(l => l.Lab_Item_Code.ToString() == order)
                    );
                if (allAccountedFor && !checkup.IsLabResultReady)
                {
                    checkup.IsLabResultReady = true;                    
                }
                _context.Checkups.Update(checkup);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        return Unit.Value;
    }
}

//public class UpsertLatestLabForWorkerCommandValidator : AbstractValidator<UpsertLatestLabForWorkerCommand>
//{
//    public UpsertLatestLabForWorkerCommandValidator()
//    {

//    }
//}
