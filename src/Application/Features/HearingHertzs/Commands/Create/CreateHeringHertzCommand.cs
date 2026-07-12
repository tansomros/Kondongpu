using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.HearingHertzs.Commands.Create;

public record CreateHeringHertzCommand : IRequest<int>
{
    public required int Hertz { get; set; }
}

public class CreateHeringHertzCommandValidator : AbstractValidator<CreateHeringHertzCommand>
{
    public CreateHeringHertzCommandValidator()
    {

    }
}

public class CreateHeringHertzCommandHandler : IRequestHandler<CreateHeringHertzCommand, int>
{
    private readonly KondongpuDatabaseContext _context;

    public CreateHeringHertzCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateHeringHertzCommand request, CancellationToken cancellationToken)
    {
        var hearingHert = new HearingHertz(
            request.Hertz
            );

        await _context.HearingHertzs.AddAsync(hearingHert, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return hearingHert.Id;
    }
}
