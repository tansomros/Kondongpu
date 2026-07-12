using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;

namespace Kondongpu.Application.Features.Lungs.Commands.Delete;

public record DeleteLungCommand : IRequest<Unit>
{
    public int Id { get; set; }
}


public class DeleteLungCommandHandler : IRequestHandler<DeleteLungCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public DeleteLungCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteLungCommand request, CancellationToken cancellationToken)
    {

        var entity = await _context.Lungs
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Lungs), request.Id);
        }

        _context.Lungs.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;

    }
}

public class DeleteLungCommandValidator : AbstractValidator<DeleteLungCommand>
{
    private readonly KondongpuDatabaseContext _context;
    public DeleteLungCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;

        RuleFor(x => x.Id).NotEmpty().WithMessage("Id ไม่สามารถเป็นค่าว่างได้").NotNull().WithMessage("โปรดระบุ Id");
    }
}
