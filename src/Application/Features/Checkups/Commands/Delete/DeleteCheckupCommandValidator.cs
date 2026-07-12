namespace Kondongpu.Application.Features.Checkups.Commands.Delete;
public class DeleteCheckupCommandValidator : AbstractValidator<DeleteCheckupCommand>
{
    public DeleteCheckupCommandValidator()
    {
        RuleFor(p => p.Id).NotEmpty().WithMessage("Id ต้องไม่เป็นค่าว่าง");
    }
}
