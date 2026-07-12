using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.Lungs.Commands.Create
{
    public class CreateLungCommand : IRequest<int>
    {
        public int CheckupId { get; set; }
        public required string VisitNumber { get; set; }
        public required int CheckupItemId { get; set; }
        public double FVC { get; set; }
        public double? FVC_Rate { get; set; }
        public double? FEV1 { get; set; }
        public double? FEV1_Rate { get; set; }
        public required string ResultAbnormal { get; set; }
        public string? RestrictionAbnormal { get; set; }
        public string? RestrictionLevel { get; set; }
        public string? ObstructionAbnormal { get; set; }
        public string? ObstructionLevel { get; set; }
        public string? CombineAbnormal { get; set; }
        public string? IsConsult { get; set; }
        public string? ResultNote { get; set; }
    }

    public class CreateLungCommmandHandler : IRequestHandler<CreateLungCommand, int>
    {
        private readonly KondongpuDatabaseContext _checkupDatabaseContext;

        public CreateLungCommmandHandler(KondongpuDatabaseContext checkupDatabaseContext)
        {
            _checkupDatabaseContext = checkupDatabaseContext;
        }

        public async Task<int> Handle(CreateLungCommand request, CancellationToken cancellationToken)
        {
            var lung = new Lung(request.CheckupId,request.VisitNumber,request.CheckupItemId,request.ResultAbnormal)
            {               
                Fvc = request.FVC, 
                Fvc_Rate = request.FVC_Rate,             
                Fev1 = request.FEV1,
                Fev1_Rate = request.FEV1_Rate,              
                RestrictionAbnormal = request.RestrictionAbnormal,
                RestrictionLevel = request.RestrictionLevel,
                ObstructionAbnormal = request.ObstructionAbnormal,
                ObstructionLevel = request.ObstructionLevel,
                CombineAbnormal = request.CombineAbnormal,
                IsConsult = request.IsConsult,
                ResultNote = request.ResultNote,
            };

            await _checkupDatabaseContext.Lungs.AddAsync(lung, cancellationToken);
            await _checkupDatabaseContext.SaveChangesAsync(cancellationToken);
            return lung.Id;
        }
    }
}
