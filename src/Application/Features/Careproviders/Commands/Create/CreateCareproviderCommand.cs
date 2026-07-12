using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.CareProviders.Commands.Create
{
    // input หรือ request
    public class CreateCareProviderCommand : IRequest<int>
    {
        public required string Code { get; set; }
        public required string NameTH { get; set; }
        public string? NameEN { get; set; }
        public string? LicenseNo { get; set; }
        public string? Position { get; set; }
        public required int CareproviderType { get; set; }
    }

    // เอา INPUT มา Process
    public class CreateCareProviderCommmandHandler : IRequestHandler<CreateCareProviderCommand, int>
    {
        private readonly KondongpuDatabaseContext _checkupDatabaseContext;

        public CreateCareProviderCommmandHandler(KondongpuDatabaseContext checkupDatabaseContext)
        {
            _checkupDatabaseContext = checkupDatabaseContext;
            //_checkupDatabaseContext = hosxpDatabaseContext;
        }

        public async Task<int> Handle(CreateCareProviderCommand request, CancellationToken cancellationToken)
        {
            var careprovider = new CareProvider(request.Code, request.NameTH, request.CareproviderType);

            if (!string.IsNullOrEmpty(request.NameEN))
            {
                careprovider.FullNameEnglish = request.NameEN;
            }
            if (!string.IsNullOrEmpty(request.LicenseNo))
            {
                careprovider.LicenseNo = request.LicenseNo;
            }

            await _checkupDatabaseContext.CareProviders.AddAsync(careprovider, cancellationToken);
            await _checkupDatabaseContext.SaveChangesAsync(cancellationToken);
            return careprovider.Id;
        }
    }
}
