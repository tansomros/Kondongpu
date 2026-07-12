using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.CheckupItems.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.CheckupItems.Queries.Get
{
    public class GetCheckupItemQuery : IRequest<CheckupItemViewModel>
    {
        public int Id { get; set; }
    }

    public class GetCheckupItemQueryHandler : IRequestHandler<GetCheckupItemQuery, CheckupItemViewModel>
    {
        private readonly IMapper _mapper;
        private readonly KondongpuDatabaseContext _checkupContext;

        public GetCheckupItemQueryHandler(KondongpuDatabaseContext checkupContext, IMapper mapper)
        {
            _checkupContext = checkupContext;
            _mapper = mapper;
        }

        public async Task<CheckupItemViewModel> Handle(GetCheckupItemQuery request, CancellationToken cancellationToken)
        {
            var checkupItem = await _checkupContext
                .CheckupItems
                .Include(c => c.CheckupGroup)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(CheckupItem), request.Id);

            return _mapper.Map<CheckupItemViewModel>(checkupItem);
        }
    }
}
