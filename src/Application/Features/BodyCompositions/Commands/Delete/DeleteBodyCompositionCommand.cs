using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;

namespace Kondongpu.Application.Features.BodyCompositions.Commands.Delete;

public record DeleteBodyCompositionCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

public class DeleteBodyCompositionCommandHandler : IRequestHandler<DeleteBodyCompositionCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public DeleteBodyCompositionCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteBodyCompositionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.BodyCompositions
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(BodyCompositions), request.Id);
        }

        _context.BodyCompositions.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

public class DeleteBodyCompositionCommandValidator : AbstractValidator<DeleteBodyCompositionCommand>
{
    private readonly KondongpuDatabaseContext _context;
    public DeleteBodyCompositionCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;

        RuleFor(x => x.Id).NotEmpty().WithMessage("Id ไม่สามารถเป็นค่าว่างได้").NotNull().WithMessage("โปรดระบุ Id");
    }
}
