namespace Kondongpu.Application.Features.RecommendationTemplates.Commands.Update;
public class UpdateRecommendationTemplateCommandValidator : AbstractValidator<UpdateRecommendationTemplateCommand>
{
    public UpdateRecommendationTemplateCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().WithMessage("Id ต้องไม่เป็นค่าว่าง");
        RuleFor(p => p.Text).NotNull().WithMessage("ข้อความต้องไม่เป็นค่าว่าง");
    }
}
