using System.Runtime.CompilerServices;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;

namespace Kondongpu.Application.Features.Recommendations.Commands.Update;

public record UpdateRecommendationCommand : IRequest<Unit>
{
    public required int Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required string CheckType { get; set; }
    public string? SexCode { get; set; }
    public string? CompareValue { get; set; }
    public double LowValue { get; set; }
    public double HighValue { get; set; }
    public required string ConclusionTh { get; set; }
    public string? ConclusionEn { get; set; }
    public string? RecommendTh { get; set; }
    public string? RecommendEn { get; set; }
    public bool IsActive { get; set; }

}



public class UpdateCommandHandler : IRequestHandler<UpdateRecommendationCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public UpdateCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateRecommendationCommand request, CancellationToken cancellationToken)
    {
        //throw new NotImplementedException();
        var recommendation = await _context.Recommendations.FirstOrDefaultAsync(b => b.Id.Equals(request.Id), cancellationToken);
        if (recommendation == null)
        {
            throw new NotFoundException(nameof(recommendation), request.Id);
        }

        recommendation.Code = request.Code;
        recommendation.Name = request.Name;
        recommendation.CheckType = request.CheckType;     
        recommendation.SexCode = request.SexCode;
        recommendation.CompareValue = request.CompareValue;
        recommendation.LowValue = request.LowValue;
        recommendation.HighValue = request.HighValue;
        recommendation.ConclusionTh = request.ConclusionTh;
        recommendation.ConclusionEn = request.ConclusionEn;
        recommendation.RecommendTh = request.RecommendTh;
        recommendation.RecommendEn = request.RecommendEn; 
        recommendation.IsActive = request.IsActive;

        _context.Recommendations.Update(recommendation);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

