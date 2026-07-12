
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.RecommendationTemplates.Commands.Update;
public class UpdateRecommendationTemplateCommand : IRequest<Unit>
{
    public required int Id { get; set; }
    public required string Text { get; set; }
}

public class UpdateRecommendationTemplateCommandHandler : IRequestHandler<UpdateRecommendationTemplateCommand, Unit>
{
    private readonly KondongpuDatabaseContext _checkupContext;
    public UpdateRecommendationTemplateCommandHandler(KondongpuDatabaseContext checkupContext)
    {
        _checkupContext = checkupContext;
    }

    public async Task<Unit> Handle(UpdateRecommendationTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await _checkupContext.RecommendationTemplates
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (template == null)
        {
            throw new NotFoundException(nameof(RecommendationTemplate), request.Id);
        }

        template.Text = request.Text;
        _checkupContext.RecommendationTemplates.Update(template);
        await _checkupContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
