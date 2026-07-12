using System.Runtime.CompilerServices;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;

namespace Kondongpu.Application.Features.Visions.Commands.Update;

public record UpdateVisionCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string? VA_Right_Result { get; set; }
    public string? VA_Right_Value { get; set; }
    public string? VA_Left_Result { get; set; }
    public string? VA_Left_Value { get; set; }
    public string? PH_Right_Result { get; set; }
    public string? PH_Right_Value { get; set; }
    public string? PH_Left_Result { get; set; }
    public string? PH_Left_Value { get; set; }
#pragma warning disable CS8618
    public string VisitNumber { get; set; }
    public string VisionRightResult { get; set; }
    public string VisionLeftResult { get; set; }
    //public string ResultNote { get; set; }
#pragma warning restore CS8618
    public string? ColorBlind { get; set; }
    public string? PressureRight { get; set; }
    public string? PressureLeft { get; set; }
    public string? Vision3D { get; set; }
    public string? Squint { get; set; }
    public string? VisualField { get; set; }
    public string? RetinaRight { get; set; }
    public string? RetinaLeft { get; set; }
    public bool IsActive { get; set; }

}



public class UpdateCommandHandler : IRequestHandler<UpdateVisionCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public UpdateCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateVisionCommand request, CancellationToken cancellationToken)
    {
        //throw new NotImplementedException();
        var vision = await _context.Visions.FirstOrDefaultAsync(b => b.Id.Equals(request.Id), cancellationToken);
        if (vision == null)
        {
            throw new NotFoundException(nameof(vision), request.Id);
        }

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
              
        vision.IsActive = request.IsActive;

        _context.Visions.Update(vision);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

