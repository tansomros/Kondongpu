using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Hearings.Commands.Create;
public class CreateHearingListCommand : IRequest<Unit>
{
    public required List<CreateHearingCommand> CreateHearingCommand { get; set; }
}
public class CreateHearingListCommandHandler : IRequestHandler<CreateHearingListCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context;

    public CreateHearingListCommandHandler(KondongpuDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CreateHearingListCommand request, CancellationToken cancellationToken)
    {   
        foreach (CreateHearingCommand hearingCreate in request.CreateHearingCommand)
        {
            var hearing = new Hearing(hearingCreate.AudiogramId, hearingCreate.Hertz, hearingCreate.LeftHz, hearingCreate.RightHz);
            _context.Hearings.Add(hearing);           
        }       
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
