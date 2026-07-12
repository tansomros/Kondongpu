using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Exceptions;
using Kondongpu.Application.Features.CareProviders.ViewModels;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.CareProviders.Queries.Get
{
    public class GetCareProviderQuery : IRequest<CareProviderViewModel>
    {
        public int Id { get; set; }
    }

    public class GetCareproviderQueryHandler : IRequestHandler<GetCareProviderQuery, CareProviderViewModel>
    {
        private readonly IMapper _mapper;
        private readonly KondongpuDatabaseContext _checkupContext;
        public GetCareproviderQueryHandler(KondongpuDatabaseContext checkupContext, IMapper mapper)
        {
            _checkupContext = checkupContext;
            _mapper = mapper;
        }

        public async Task<CareProviderViewModel> Handle(GetCareProviderQuery request, CancellationToken cancellationToken)
        {
            var careprovider = await _checkupContext.CareProviders
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(CareProvider), request.Id);

            return _mapper.Map<CareProviderViewModel>(careprovider);
        }
    }
}
