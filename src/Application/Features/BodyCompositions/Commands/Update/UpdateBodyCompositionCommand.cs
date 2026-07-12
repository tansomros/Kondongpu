using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;

namespace Kondongpu.Application.Features.BodyCompositions.Commands.Update;

public record UpdateBodyCompositionCommand : IRequest<Unit>
{
    public required int Id { get; set; }
    public int CheckupId { get; set; }
    public required string VisitNumber { get; set; }
    public required int CheckupItemId { get; set; }
    public string? Bmr { get; set; }
    public string? BmrNote { get; set; }
    public string? BodyWater { get; set; }
    public string? BodyWaterNote { get; set; }
    public string? VisceralFat { get; set; }
    public string? VisceralFatNote { get; set; }
    public string? BodyFat { get; set; }
    public string? BodyFatNote { get; set; }
    public string? FatRate { get; set; }
    public string? FatRateNote { get; set; }
    public string? MuscleMass { get; set; }
    public string? MuscleMassNote { get; set; }
    public bool IsActive { get; set; }
}

public class UpdateBodyCompositionCommandHandler : IRequestHandler<UpdateBodyCompositionCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public UpdateBodyCompositionCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateBodyCompositionCommand request, CancellationToken cancellationToken)
    {
        var bodyComposition = await _context.BodyCompositions.FirstOrDefaultAsync(b => b.Id.Equals(request.Id), cancellationToken);
        if (bodyComposition == null)
        {
            throw new NotFoundException(nameof(bodyComposition), request.Id);
        }

        bodyComposition.Bmr = request.Bmr;
        bodyComposition.BmrNote = request.BmrNote;
        bodyComposition.BodyWater = request.BodyWater;
        bodyComposition.BodyWaterNote = request.BodyWaterNote;
        bodyComposition.VisceralFat = request.VisceralFat;
        bodyComposition.VisceralFatNote = request.VisceralFatNote;
        bodyComposition.BodyFat = request.BodyFat;
        bodyComposition.BodyFatNote = request.BodyFatNote;
        bodyComposition.FatRate = request.FatRate;
        bodyComposition.FatRateNote = request.FatRateNote;
        bodyComposition.MuscleMass = request.MuscleMass;
        bodyComposition.MuscleMassNote = request.MuscleMassNote;
        bodyComposition.IsActive = request.IsActive;

        _context.BodyCompositions.Update(bodyComposition);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
