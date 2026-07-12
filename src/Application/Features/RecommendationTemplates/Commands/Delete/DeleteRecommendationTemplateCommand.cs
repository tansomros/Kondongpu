
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.RecommendationTemplates.Commands.Delete;
public class DeleteRecommendationTemplateCommand : IRequest<Unit>
{
    public required int Id { get; set; }
}

public class DeleteRecommendationTemplateCommandHandler : IRequestHandler<DeleteRecommendationTemplateCommand, Unit>
{
    private readonly KondongpuDatabaseContext _checkupContext;
    public DeleteRecommendationTemplateCommandHandler(KondongpuDatabaseContext checkupContext)
    {
        _checkupContext = checkupContext;
    }

    public async Task<Unit> Handle(DeleteRecommendationTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await _checkupContext.RecommendationTemplates
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (template == null)
        {
            throw new NotFoundException(nameof(RecommendationTemplate), request.Id);
        }

        _checkupContext.RecommendationTemplates.Remove(template);
        await _checkupContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
