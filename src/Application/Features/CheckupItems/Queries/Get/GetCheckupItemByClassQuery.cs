using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Features.CheckupItems.ViewModels;
using Kondongpu.Application.Features.Checkups.Queries;

namespace Kondongpu.Application.Features.CheckupItems.Queries.Get;
public class GetCheckupItemByClassQuery : IRequest<CheckupItemListViewModel>
{    
    public required string CheckupClassCode { get; set; }
}

public class GetCheckupItemByItemClassQueryHandler : IRequestHandler<GetCheckupItemByClassQuery, CheckupItemListViewModel>
{
    private readonly IMapper _mapper;
    private readonly KondongpuDatabaseContext _context;

    public GetCheckupItemByItemClassQueryHandler(IMapper mapper, KondongpuDatabaseContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<CheckupItemListViewModel> Handle(GetCheckupItemByClassQuery request, CancellationToken cancellationToken)
    {
        // TODO ปรับ การ Inclue checkupGroup เนื่องจาก ว่า ปัจจุบัน มันจะเป็นค่าว่างไม่ได้
        var checkupItems = await _context
            .CheckupItems.AsNoTracking().Include(item => item.CheckupGroup).ThenInclude(group => group.CheckupClass)
            .Where(c => c.CheckupGroup.CheckupClass.Code == request.CheckupClassCode)
            .ToListAsync(cancellationToken);

        var checkupItemList = _mapper.Map<List<CheckupItemViewModel>>(checkupItems);

        return new CheckupItemListViewModel()
        {
            CheckupItems = checkupItemList
        };
    }
}
