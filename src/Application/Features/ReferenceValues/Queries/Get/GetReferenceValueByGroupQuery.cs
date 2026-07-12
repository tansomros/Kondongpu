using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.ReferenceValues.ViewModels;
using Kondongpu.Application.Features.Checkups.Queries;

#pragma warning disable CS0618
namespace Kondongpu.Application.Features.ReferenceValues.Queries.Get;
[Obsolete("ใช้ SmartEnum จาก Domain.Enums แทน — ดู LookupRegistry.cs")]
public class GetReferenceValueByGroupQuery : IRequest<ReferenceValueListViewModel>
{    
    public required int ReferenceGroupId { get; set; }
}

[Obsolete("ใช้ SmartEnum จาก Domain.Enums แทน — ดู LookupRegistry.cs")]
public class GetReferenceValueByGroupQueryHandler : IRequestHandler<GetReferenceValueByGroupQuery, ReferenceValueListViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetReferenceValueByGroupQueryHandler(IMapper mapper, KondongpuDatabaseContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ReferenceValueListViewModel> Handle(GetReferenceValueByGroupQuery request, CancellationToken cancellationToken)
    {
        var referenceValues = await _context
            .ReferenceValues.AsNoTracking().Include(c => c.ReferenceGroup)
            .Where(c => c.ReferenceGroupId == request.ReferenceGroupId)
            .ToListAsync(cancellationToken);

        var referenceValueList = _mapper.Map<List<ReferenceValueViewModel>>(referenceValues);

        return new ReferenceValueListViewModel()
        {
            ReferenceValues = referenceValueList
        };
    }
}
