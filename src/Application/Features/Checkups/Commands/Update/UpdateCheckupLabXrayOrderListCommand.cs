
using Kondongpu.Application.Common.Interfaces;

namespace Kondongpu.Application.Features.Checkups.Commands.Update;
public class UpdateCheckupLabXrayOrderListCommand : IRequest<Unit>
{
    public required List<UpdateCheckupLabXrayOrder> UpdateCheckupLabXrayOrders { get; set; }
}

public class UpdateCheckupLabXrayOrderListCommandHandler(KondongpuDatabaseContext context) : IRequestHandler<UpdateCheckupLabXrayOrderListCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context = context;

    public async Task<Unit> Handle(UpdateCheckupLabXrayOrderListCommand request, CancellationToken cancellationToken)
    {
        var checkupItemCode = await _context.CheckupItems.Select( s => s.LabItemCode).ToListAsync(cancellationToken);

        foreach ( var item in request.UpdateCheckupLabXrayOrders)
        {
            var checkup = await _context.Checkups.FirstOrDefaultAsync(
                s => s.VisitNumber == item.VisitNumber, cancellationToken
                );

            if ( checkup != null )
            {
                if (item.LabOrder.Count > 0)
                {
                    var laborder = checkup.LabOrder;
                    var addedLaborder = item.LabOrder.Where(checkupItemCode.Contains).ToList();
                    var updatedLabOrder = laborder.Union(addedLaborder).ToList();
                    checkup.LabOrder = updatedLabOrder;
                }

                if (item.XrayOrder.Count > 0)
                {
                    var xrayOrder = checkup.XrayOrder;
                    var addedXrayOrder = item.XrayOrder.Where(checkupItemCode.Contains).ToList();
                    var updatedXrayOrder = xrayOrder.Union(addedXrayOrder).ToList();
                    checkup.XrayOrder = updatedXrayOrder;
                }

                if (item.ServiceOrder.Count > 0)
                {
                    var serviceOrder = checkup.ServiceOrder;
                    var addedServiceOrder = item.ServiceOrder.Where(checkupItemCode.Contains).ToList();
                    var updatedServiceOrder = serviceOrder.Union(addedServiceOrder).ToList();
                    checkup.ServiceOrder = updatedServiceOrder;
                }

                _context.Checkups.Update(checkup);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        return Unit.Value;
    }
}


