using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.CheckupGroups.Commands.Create
{
    public class CreateCheckupGroupCommand : IRequest<int>
    {
        public required int Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required int ClassId { get; set; }
        public int Sort { get; set; }
    }
    public class CreateCheckupGroupCommmandHandler : IRequestHandler<CreateCheckupGroupCommand, int>
    {
        private readonly KondongpuDatabaseContext _checkupDatabaseContext;

        public CreateCheckupGroupCommmandHandler(KondongpuDatabaseContext checkupDatabaseContext)
        {
            _checkupDatabaseContext = checkupDatabaseContext;
        }

        public async Task<int> Handle(CreateCheckupGroupCommand request, CancellationToken cancellationToken)
        {
            var group = new CheckupGroup(request.Id, request.Code, request.Name,request.ClassId,request.Sort);

            await _checkupDatabaseContext.CheckupGroups.AddAsync(group, cancellationToken);
            await _checkupDatabaseContext.SaveChangesAsync(cancellationToken);
            return group.Id;
        }
    }
}
