using Kondongpu.Application.Common.Interfaces;

namespace Kondongpu.Application.Features.CareProviders.Commands.Create
{
    public class CreateCareProviderCommandValidator : AbstractValidator<CreateCareProviderCommand>
    {
        private readonly KondongpuDatabaseContext _context;

        public CreateCareProviderCommandValidator(KondongpuDatabaseContext context)
        {
            _context = context;
            RuleFor(p => p.Code).NotNull().WithMessage("รหัส ต้องไม่ว่าง");
            RuleFor(p => p.NameTH).NotNull().WithMessage("ชื่อภาษาไทย ต้องไม่ว่าง");
        }
    }
}
