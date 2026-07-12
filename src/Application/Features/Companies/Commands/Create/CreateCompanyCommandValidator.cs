using Kondongpu.Application.Common.Interfaces;

namespace Kondongpu.Application.Features.Companies.Commands.Create
{
    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        private readonly IKondongpuDatabaseContext _context;
        public  CreateCompanyCommandValidator(IKondongpuDatabaseContext context)
        {
            _context = context;

            RuleFor(p => p.CompanyCode)
                    .NotEmpty().WithMessage("CompanyCode ต้องไม่ว่าง")
                    .NotNull().WithMessage("โปรดระบุ CompanyCode");
          
        }
    }
}
