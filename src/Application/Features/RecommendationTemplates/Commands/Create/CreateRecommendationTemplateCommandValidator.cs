namespace Kondongpu.Application.Features.RecommendationTemplates.Commands.Create;
public class CreateRecommendationTemplateCommandValidator : AbstractValidator<CreateRecommendationTemplateCommand>
{
    public CreateRecommendationTemplateCommandValidator()
    {
        RuleFor(p => p.Text).NotNull().WithMessage("ข้อความต้องไม่เป็นค่าว่าง");
    }
}
