using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Users.Commands.Delete;

public record DeleteUserCommand : IRequest<Unit>
{
    public required int Id { get; set; }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IKondongpuDatabaseContext _context;

    public DeleteUserCommandHandler(IKondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var User = await _context.Users.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Users), request.Id);

        _context.Users.Remove(User);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
