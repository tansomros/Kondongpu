using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.CareProviders.Commands.Update;
public class UpsertLatestUpdateProviderForWorkerCommand : IRequest<Unit>
{
    public required List<UpsertProviderCommand> UpsertProviders { get; set; }
}

public class UpsertProviderCommand
{
    public required string Code { get; set; }
    public required string FullNameThai {  get; set; }
    public string? FullNameEnglish { get; set; } = null!;
    public string? LicenseNo { get; set;} = null!;
    public string? NationId { get; set;} = null!;
    public string? PositionName { get; set; } = null!;
    public required int CareProviderTypeId { get; set;}
    public string? CareProviderTypeName { get;set; } = null!;
    public string? LoginName { get; set; } = null!;
}

public class UpsertLatestUpdateProviderForWorkerCommandHandler(KondongpuDatabaseContext context) 
    : IRequestHandler<UpsertLatestUpdateProviderForWorkerCommand, Unit>
{
    private readonly KondongpuDatabaseContext _context = context;

    public async Task<Unit> Handle(UpsertLatestUpdateProviderForWorkerCommand request, CancellationToken cancellationToken)
    {
        var careProviderList = new List<CareProvider>();
        foreach (var item in request.UpsertProviders)
        {
            var careProvider = await _context.CareProviders.FirstOrDefaultAsync(
                c => c.Code == item.Code, cancellationToken);

            if (careProvider != null)
            {
                careProvider.Code = item.Code;
                careProvider.FullNameThai = item.FullNameThai;
                careProvider.FullNameEnglish = item.FullNameEnglish;
                careProvider.LicenseNo = item.LicenseNo;
                careProvider.NationalId = item.NationId;
                careProvider.CareProviderTypeId = item.CareProviderTypeId;
                careProvider.CareProviderTypeName = item.CareProviderTypeName;          
                careProvider.PositionName = item.PositionName;

                _context.CareProviders.Update(careProvider);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                careProviderList.Add(
                    new CareProvider(
                        item.Code, 
                        item.FullNameThai, 
                        item.CareProviderTypeId
                        )
                    {
                        CareProviderTypeName = item.CareProviderTypeName,
                        FullNameEnglish = item.FullNameEnglish,
                        LicenseNo = item.LicenseNo,
                        NationalId = item.NationId,
                        PositionName = item.PositionName,   
                    });
            }
        }

        if(careProviderList.Count != 0)
        {
            await _context.CareProviders.AddRangeAsync(careProviderList, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}
