using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;

namespace Kondongpu.Application.Features.Recommendations.Commands.Delete;

public record DeleteRecommendationCommand : IRequest<Unit>
{
    public int Id { get; set; }
}


public class DeleteRecommendationCommandHandler : IRequestHandler<DeleteRecommendationCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public DeleteRecommendationCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteRecommendationCommand request, CancellationToken cancellationToken)
    {

        var entity = await _context.Recommendations
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Recommendations), request.Id);
        }

        _context.Recommendations.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;

    }
}

public class DeleteRecommendationCommandValidator : AbstractValidator<DeleteRecommendationCommand>
{
    private readonly KondongpuDatabaseContext _context;
    public DeleteRecommendationCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;

        RuleFor(x => x.Id).NotEmpty().WithMessage("Id ไม่สามารถเป็นค่าว่างได้").NotNull().WithMessage("โปรดระบุ Id");
    }
}
