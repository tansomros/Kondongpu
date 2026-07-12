using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.CheckupItems.Commands.Create
{
    // input หรือ request
    public class CreateCheckupItemCommand : IRequest<int>
    {
        public required int Id { get; set; }
        public required string Code { get; set; }
        public string? ItemCode { get; set; }
        public string? HosxpICode { get; set; }
        public required string DisplayName { get; set; }
        public string? Description { get; set; }
        public string? CumulativeName { get; set; }
        public string? CumulativeGroup { get; set; }
        public int CumulativeSort { get; set; }
        public required int CheckupGroupId { get; set; } 
        public int Sort { get; set; }
        public bool IsDisplayPrint { get; set; }
    }

    // เอา INPUT มา Process
    public class CreateCheckupItemCommmandHandler : IRequestHandler<CreateCheckupItemCommand, int>
    {
        private readonly KondongpuDatabaseContext _checkupDatabaseContext;

        public CreateCheckupItemCommmandHandler(KondongpuDatabaseContext checkupDatabaseContext)
        {
            _checkupDatabaseContext = checkupDatabaseContext;
        }


        public async Task<int> Handle(CreateCheckupItemCommand request, CancellationToken cancellationToken)
        {
            var checkupItem = new CheckupItem(
                request.Id,
                request.Code,
                request.ItemCode,
                request.DisplayName,
                request.Description,
                request.CheckupGroupId, 
                request.Sort,
                request.CumulativeName,
                request.CumulativeGroup,
                request.CumulativeSort,
                request.IsDisplayPrint
                );

            var checkupGroup = await _checkupDatabaseContext
                .CheckupGroups
                .AsNoTracking()
                .FirstOrDefaultAsync(cg => cg.Id == request.CheckupGroupId, cancellationToken);

            if (checkupGroup != null)
            {
                checkupItem.CheckupGroupId = checkupGroup.Id;
            }

            checkupItem.LabItemCode = request.HosxpICode;
            checkupItem.Description = request.Description;
            checkupItem.CumulativeName = request.CumulativeName;
            checkupItem.CumulativeGroup = request.CumulativeGroup;
            checkupItem.CheckupGroupId = request.CheckupGroupId; 
            checkupItem.Sort = request.Sort;
            await _checkupDatabaseContext.CheckupItems.AddAsync(checkupItem, cancellationToken);
            await _checkupDatabaseContext.SaveChangesAsync(cancellationToken);
            return checkupItem.Id;
        }
    }
}
