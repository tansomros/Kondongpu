using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Domain.Entities;
using Kondongpu.Domain.ValueObjects;

namespace Kondongpu.Application.Features.Checkups.Commands.Update;

public record SaveCheckupFinalReportCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public required FinalReportCommand FinalReports { get; set; }
}

public class CreateCommandHandler : IRequestHandler<SaveCheckupFinalReportCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public CreateCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(SaveCheckupFinalReportCommand request, CancellationToken cancellationToken)
    {
        var checkup = await _context.Checkups.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Checkup), request.Id);
        //
        //Add lab report
        //
        FinalReport finalReport = new();
        foreach (FinalLabCommand finalLabCommand in request.FinalReports.Labs)
        {
            var finalLab = new FinalLab()
            {
                CheckupGroupCode = finalLabCommand.CheckupGroupCode,
                Name = finalLabCommand.Name,
                ReferenceRange = finalLabCommand.ReferenceRange,
                IsAbnormal = finalLabCommand.IsAbnormal,
                ResultValue = finalLabCommand.ResultValue
            };

            finalReport.Labs.Add(finalLab);
        }
        //
        //Add vision report
        //
        var visionCommand = request.FinalReports.Vision;
        var vision = new FinalVision()
        {
            ColorBlind = visionCommand?.ColorBlind,
            PH_Left_Note = visionCommand?.ColorBlind,
            PH_Right_Note = visionCommand?.ColorBlind,
            PressureLeft = visionCommand?.ColorBlind,
            PressureRight = visionCommand?.ColorBlind,
            RetinaLeft = visionCommand?.ColorBlind,
            RetinaRight = visionCommand?.ColorBlind,
            Squint = visionCommand?.Squint,
            VA_Left_Note = visionCommand?.VA_Left_Note,
            VA_Right_Note = visionCommand?.VA_Right_Note,
            Vision3D = visionCommand?.Vision3D,
            VisualField = visionCommand?.VisualField,            
        };        
        finalReport.Vision = vision;    
        //
        //
        //
        checkup.FinalReport = finalReport;
        _context.Checkups.Update(checkup);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
