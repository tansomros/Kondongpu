using Kondongpu.Application.Common.Interfaces;

namespace Kondongpu.Application.Features.Recommendations.Commands.Update
{
    public class UpdateRecommendationCommandValidator : AbstractValidator<UpdateRecommendationCommand>
    {
        private readonly KondongpuDatabaseContext _context;
        public UpdateRecommendationCommandValidator(KondongpuDatabaseContext context)
        {
            _context = context;

            RuleFor(x => x.Id).NotEmpty().WithMessage("Id ไม่สามารถเป็นค่าว่างได้").NotNull().WithMessage("โปรดระบุ Id");
            RuleFor(p => p.Code).NotEmpty().WithMessage("Code ไม่สามารถเป็นค่าว่างได้").NotNull().WithMessage("โปรดระบุ Code");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name ไม่สามารถเป็นค่าว่างได้").NotNull().WithMessage("โปรดระบุ Name");
            RuleFor(p => p.CheckType).NotEmpty().WithMessage("CheckType ไม่สามารถเป็นค่าว่างได้").NotNull().WithMessage("โปรดระบุ CheckType");
            RuleFor(p => p.LowValue).NotEmpty().WithMessage("LowValue ไม่สามารถเป็นค่าว่างได้").NotNull().WithMessage("โปรดระบุ LowValue");
            RuleFor(p => p.HighValue).NotEmpty().WithMessage("HighValue ไม่สามารถเป็นค่าว่างได้").NotNull().WithMessage("โปรดระบุ HighValue");
            RuleFor(p => p.ConclusionTh).NotEmpty().WithMessage("ConclusionTh ไม่สามารถเป็นค่าว่างได้").NotNull().WithMessage("โปรดระบุ ConclusionTh");
            RuleFor(p => p.IsActive).NotEmpty().WithMessage("สถานะ ไม่สามารถเป็นค่าว่างได้").NotNull().WithMessage("โปรดระบุ สถานะ");
        }
    }
}
