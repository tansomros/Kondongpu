using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Domain.Entities;

namespace Kondongpu.Application.Features.BodyCompositions.Commands.Create
{
    public class CreateBodyCompositionCommand : IRequest<int>
    {
        public int CheckupId { get; set; }
        public required string VisitNumber { get; set; }
        public required int CheckupItemId { get; set; }
        public string? Bmr { get; set; }
        public string? BmrNote { get; set; }
        public string? BodyWater { get; set; }
        public string? BodyWaterNote { get; set; }
        public string? VisceralFat { get; set; }
        public string? VisceralFatNote { get; set; }
        public string? BodyFat { get; set; }
        public string? BodyFatNote { get; set; }
        public string? FatRate { get; set; }
        public string? FatRateNote { get; set; }
        public string? MuscleMass { get; set; }
        public string? MuscleMassNote { get; set; }

    }

    public class CreateBodyCompositionCommandHandler : IRequestHandler<CreateBodyCompositionCommand, int>
    {
        private readonly KondongpuDatabaseContext _checkupDatabaseContext;

        public CreateBodyCompositionCommandHandler(KondongpuDatabaseContext checkupDatabaseContext)
        {
            _checkupDatabaseContext = checkupDatabaseContext;
        }

        public async Task<int> Handle(CreateBodyCompositionCommand request, CancellationToken cancellationToken)
        {
            var bodyComposition = new BodyComposition(request.CheckupId, request.VisitNumber, request.CheckupItemId)
            {
                Bmr = request.Bmr,
                BmrNote = request.BmrNote,
                BodyWater = request.BodyWater,
                BodyWaterNote = request.BodyWaterNote,
                VisceralFat = request.VisceralFat,
                VisceralFatNote = request.VisceralFatNote,
                BodyFat = request.BodyFat,
                BodyFatNote = request.BodyFatNote,
                FatRate = request.FatRate,
                FatRateNote = request.FatRateNote,
                MuscleMass = request.MuscleMass,
                MuscleMassNote = request.MuscleMassNote,
    };

            await _checkupDatabaseContext.BodyCompositions.AddAsync(bodyComposition, cancellationToken);
            await _checkupDatabaseContext.SaveChangesAsync(cancellationToken);
            return bodyComposition.Id;
        }
    }
}
