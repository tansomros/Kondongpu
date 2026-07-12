using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;

namespace Kondongpu.Application.Features.Dentals.Commands.Update;

public record UpdateDentalCommand : IRequest<Unit>
{
    public required int Id { get; set; }
    public int CheckupId { get; set; }
    public required string VisitNumber { get; set; }
    public required int CheckupItemId { get; set; }
    public required string ResultValue { get; set; }
    public string? ResultNote { get; set; }
    public bool Scaling { get; set; }
    public bool Gingivitis { get; set; }
    public bool Decay { get; set; }
    public bool Fluoride { get; set; }
    public bool Sealant { get; set; }
    public string? SealantNote { get; set; }
    public bool Filling { get; set; }
    public string? FillingNote { get; set; }
    public bool Extraction { get; set; }
    public string? ExtractionNote { get; set; }
    public bool IsActive { get; set; }
}

public class UpdateDentalCommandHandler : IRequestHandler<UpdateDentalCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public UpdateDentalCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateDentalCommand request, CancellationToken cancellationToken)
    {
        var dental = await _context.Dentals.FirstOrDefaultAsync(b => b.Id.Equals(request.Id), cancellationToken);
        if (dental == null)
        {
            throw new NotFoundException(nameof(dental), request.Id);
        }

        dental.ResultValue = request.ResultValue;
        dental.ResultNote = request.ResultNote;
        dental.Scaling = request.Scaling;
        dental.Gingivitis = request.Gingivitis;
        dental.Decay = request.Decay;
        dental.Fluoride = request.Fluoride;
        dental.Sealant = request.Sealant;
        dental.SealantNote = request.SealantNote;
        dental.Filling = request.Filling;
        dental.FillingNote = request.FillingNote;
        dental.Extraction = request.Extraction;
        dental.ExtractionNote = request.ExtractionNote;
        dental.IsActive = request.IsActive;

        _context.Dentals.Update(dental);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
