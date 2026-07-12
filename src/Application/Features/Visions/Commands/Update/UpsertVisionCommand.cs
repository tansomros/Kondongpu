using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Visions.Commands.Update;

public record UpsertVisionCommand : IRequest<Unit>
{
    public required string VisitNumber { get; set; }
    public required int CheckupId { get; set; }
    public required string CheckupClassCode { get; set; }   

    public string? VA_Right_Result { get; set; }
    public string? VA_Right_Value { get; set; }
    public string? VA_Left_Result { get; set; }
    public string? VA_Left_Value { get; set; }
    public string? PH_Right_Result { get; set; }
    public string? PH_Right_Value { get; set; }
    public string? PH_Left_Result { get; set; }
    public string? PH_Left_Value { get; set; }
    public required string VisionRightResult { get; set; }
    public required string VisionLeftResult { get; set; }
    public string? ColorBlind { get; set; }
    public string? PressureRight { get; set; }
    public string? PressureLeft { get; set; }
    public string? Vision3D { get; set; }
    public string? Squint { get; set; }
    public string? VisualField { get; set; }
    public string? RetinaRight { get; set; }
    public string? RetinaLeft { get; set; }
}

public class UpsertVisionCommandHandler : IRequestHandler<UpsertVisionCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public UpsertVisionCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpsertVisionCommand request, CancellationToken cancellationToken)
    {

        var checkupItems = await _context
            .CheckupItems.AsNoTracking().Include(item => item.CheckupGroup).ThenInclude(group => group.CheckupClass)
            .Where(c => c.CheckupGroup.CheckupClass.Code == request.CheckupClassCode)
            .ToListAsync(cancellationToken);

        var checkupItemId = checkupItems.First().Id;

        var vision = await _context.Visions.FirstOrDefaultAsync(
            s => s.VisitNumber == request.VisitNumber && s.CheckupId == request.CheckupId && s.CheckupItemId == checkupItemId
            , cancellationToken);

        if (vision == null)
        {
            var newVision = new Vision(request.CheckupId, request.VisitNumber, checkupItemId, request.VisionRightResult, request.VisionLeftResult, string.Empty)
            {
                //VA_Right_Result = request.VA_Right_Result,
                VA_Right_Value = request.VA_Right_Value,
                //VA_Left_Result = request.VA_Left_Result,
                VA_Left_Value = request.VA_Left_Value,

                //PH_Right_Result = request.PH_Right_Result,
                PH_Right_Value = request.PH_Right_Value,
                //PH_Left_Result = request.PH_Left_Result,
                PH_Left_Value = request.PH_Left_Value,

                ColorBlind = request.ColorBlind,
                PressureRight = request.PressureRight,
                PressureLeft = request.PressureLeft,
                Vision3D = request.Vision3D,
                Squint = request.Squint,
                VisualField = request.VisualField,
                RetinaRight = request.RetinaRight,
                RetinaLeft = request.RetinaLeft,
                CreatedOn = DateTime.Now,
            };

            await _context.Visions.AddAsync(newVision, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            vision.VisionRightResult = request.VisionRightResult;
            vision.VisionLeftResult = request.VisionLeftResult;
            //vision.ResultNote = request.ResultNote;

            //vision.VA_Right_Result = request.VA_Right_Result;
            vision.VA_Right_Value = request.VA_Right_Value;
            //vision.VA_Left_Result = request.VA_Left_Result;
            vision.VA_Left_Value = request.VA_Left_Value;

            //vision.PH_Right_Result = request.PH_Right_Result;
            vision.PH_Right_Value = request.PH_Right_Value;
            //vision.PH_Left_Result = request.PH_Left_Result;
            vision.PH_Left_Value = request.PH_Left_Value;

            vision.ColorBlind = request.ColorBlind;
            vision.PressureRight = request.PressureRight;
            vision.PressureLeft = request.PressureLeft;
            vision.Vision3D = request.Vision3D;
            vision.Squint = request.Squint;
            vision.VisualField = request.VisualField;
            vision.RetinaRight = request.RetinaRight;
            vision.RetinaLeft = request.RetinaLeft;

            _context.Visions.Update(vision);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}
