using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;

namespace Kondongpu.Application.Features.Lungs.Commands.Update;

public record UpdateLungCommand : IRequest<Unit>
{
    public required int Id { get; set; }
    public int CheckupId { get; set; }
    public required string VisitNumber { get; set; }
    public required int CheckupItemId { get; set; }
    public double FVC { get; set; }
    public double? FVC_Rate { get; set; }
    public double? FEV1 { get; set; }
    public double? FEV1_Rate { get; set; }
    public required string ResultAbnormal { get; set; }
    public string? RestrictionAbnormal { get; set; }
    public string? RestrictionLevel { get; set; }
    public string? ObstructionAbnormal { get; set; }
    public string? ObstructionLevel { get; set; }
    public string? CombineAbnormal { get; set; }
    public string? IsConsult { get; set; }
    public string? ResultNote { get; set; }
    public bool IsActive { get; set; }

}



public class UpdateCommandHandler : IRequestHandler<UpdateLungCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public UpdateCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateLungCommand request, CancellationToken cancellationToken)
    {
        //throw new NotImplementedException();
        var lung = await _context.Lungs.FirstOrDefaultAsync(b => b.Id.Equals(request.Id), cancellationToken);
        if (lung == null)
        {
            throw new NotFoundException(nameof(lung), request.Id);
        }

        lung.Fvc = request.FVC;
        lung.Fvc_Rate = request.FVC_Rate;
        lung.Fev1 = request.FEV1;
        lung.Fev1_Rate = request.FEV1_Rate;
        lung.ResultAbnormal = request.ResultAbnormal;
        lung.RestrictionAbnormal = request.RestrictionAbnormal;
        lung.RestrictionLevel = request.RestrictionLevel;
        lung.ObstructionAbnormal = request.ObstructionAbnormal;
        lung.ObstructionLevel = request.ObstructionLevel;
        lung.CombineAbnormal = request.CombineAbnormal;
        lung.IsConsult = request.IsConsult;
        lung.ResultNote = request.ResultNote;
        lung.IsActive = request.IsActive;
        //lung.CreatedOn = DateTime.Now;
        //lung.LastModified = DateTime.Now;

        _context.Lungs.Update(lung);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

