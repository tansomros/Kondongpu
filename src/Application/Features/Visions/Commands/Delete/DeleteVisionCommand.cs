using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;

namespace Kondongpu.Application.Features.Visions.Commands.Delete;

public record DeleteVisionCommand : IRequest<Unit>
{
    public int Id { get; set; }
}


public class DeleteVisionCommandHandler : IRequestHandler<DeleteVisionCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public DeleteVisionCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteVisionCommand request, CancellationToken cancellationToken)
    {

        var entity = await _context.Visions
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Visions), request.Id);
        }

        _context.Visions.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;

    }
}

public class DeleteVisionCommandValidator : AbstractValidator<DeleteVisionCommand>
{
    private readonly KondongpuDatabaseContext _context;
    public DeleteVisionCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;

        RuleFor(x => x.Id).NotEmpty().WithMessage("Id ไม่สามารถเป็นค่าว่างได้").NotNull().WithMessage("โปรดระบุ Id");
    }
}
