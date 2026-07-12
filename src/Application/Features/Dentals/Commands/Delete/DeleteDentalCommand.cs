using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;

namespace Kondongpu.Application.Features.Dentals.Commands.Delete;

public record DeleteDentalCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

public class DeleteDentalCommandHandler : IRequestHandler<DeleteDentalCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public DeleteDentalCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteDentalCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Dentals
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Dentals), request.Id);
        }

        _context.Dentals.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

public class DeleteDentalCommandValidator : AbstractValidator<DeleteDentalCommand>
{
    private readonly KondongpuDatabaseContext _context;
    public DeleteDentalCommandValidator(KondongpuDatabaseContext context)
    {
        _context = context;

        RuleFor(x => x.Id).NotEmpty().WithMessage("Id ไม่สามารถเป็นค่าว่างได้").NotNull().WithMessage("โปรดระบุ Id");
    }
}
