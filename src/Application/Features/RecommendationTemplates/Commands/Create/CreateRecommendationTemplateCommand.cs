
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.RecommendationTemplates.Commands.Create;
public class CreateRecommendationTemplateCommand : IRequest<int>
{
    public required string Text { get; set; }
}

public class CreateRecommendationTemplateCommandHandler : IRequestHandler<CreateRecommendationTemplateCommand, int>
{
    private readonly KondongpuDatabaseContext _checkupContext;
    public CreateRecommendationTemplateCommandHandler(KondongpuDatabaseContext checkupContext)
    {
        _checkupContext = checkupContext;
    }

    public async Task<int> Handle(CreateRecommendationTemplateCommand request, CancellationToken cancellationToken)
    {
        var entity = new RecommendationTemplate(request.Text);

        _checkupContext.RecommendationTemplates.Add(entity);
        await _checkupContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
