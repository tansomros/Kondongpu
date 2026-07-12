
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Checkups.Commands.Update;
public class UpsertCheckupLabXrayIsResultReadyListCommand : IRequest<Unit>
{
    public DateOnly? StartDate { get; set; }
    public required List<UpsertCheckupLabXrayIsResultReadyCommand> UpsertCheckupLabXrayIsResultReadyCommands { get; set; }
}

public class UpsertCheckupLabXrayIsResultReadyListCommandHandler : IRequestHandler<UpsertCheckupLabXrayIsResultReadyListCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;
    public UpsertCheckupLabXrayIsResultReadyListCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(UpsertCheckupLabXrayIsResultReadyListCommand request, CancellationToken cancellationToken)
    {
        var checkups = await _context.Checkups
            .Where(s => s.IsLabResultReady==false || s.IsXrayResultReady ==false) //s.VisitDate == request.StartDate  &&
            .ToListAsync(cancellationToken);

        foreach (Checkup checkup in checkups)
        {
            var inputVisit = request.UpsertCheckupLabXrayIsResultReadyCommands.FirstOrDefault(s => s.VisitNumber == checkup.VisitNumber);
            if (inputVisit != null) 
            { 
                checkup.IsLabResultReady = inputVisit.IsLabResultReady;
                checkup.IsXrayResultReady = inputVisit.IsXrayResultReady;

                _context.Checkups.Update(checkup);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        

        return Unit.Value;
    }
}
