using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.CheckupClasses.Commands.Create
{
    public class CreateCheckupClassCommand : IRequest<int>
    {
        public required int Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public int Sort { get; set; }
    }
    public class CreateCheckupClassCommmandHandler : IRequestHandler<CreateCheckupClassCommand, int>
    {
        private readonly KondongpuDatabaseContext _checkupDatabaseContext;

        public CreateCheckupClassCommmandHandler(KondongpuDatabaseContext checkupDatabaseContext)
        {
            _checkupDatabaseContext = checkupDatabaseContext;
        }

        public async Task<int> Handle(CreateCheckupClassCommand request, CancellationToken cancellationToken)
        {
            var Class = new CheckupClass(request.Id, request.Code, request.Name,request.Sort);

            await _checkupDatabaseContext.CheckupClasses.AddAsync(Class, cancellationToken);
            await _checkupDatabaseContext.SaveChangesAsync(cancellationToken);
            return Class.Id;
        }
    }
}
