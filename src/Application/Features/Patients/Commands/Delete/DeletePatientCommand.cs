using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;

namespace Kondongpu.Application.Features.Patients.Commands.Delete;

public record DeletePatientCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
public class DeleteCommandHandler : IRequestHandler<DeletePatientCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public DeleteCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {

        var entity = await _context.Patients
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Patients), request.Id);
        }

        _context.Patients.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;

    }
}
