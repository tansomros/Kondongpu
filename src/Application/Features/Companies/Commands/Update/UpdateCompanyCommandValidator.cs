using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Kondongpu.Application.Common.Interfaces;

namespace Kondongpu.Application.Features.Companies.Commands.Update
{
    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
    {
        private readonly IKondongpuDatabaseContext _context;
        public UpdateCompanyCommandValidator(IKondongpuDatabaseContext context)
        {
            _context = context;

            RuleFor(x => x.Id).NotEmpty()
                .WithMessage("Id ไม่สามารถเป็นค่าว่างได้")
                .NotNull().WithMessage("โปรดระบุ Id")
                .MustAsync(BeExistAsync).WithMessage("ไม่พบข้อมูล");
        }

        public async Task<bool> BeExistAsync(int id, CancellationToken cancellationToken)
        {
            var company = await _context
                .Company
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

            return company != null;
        }
    }
}
