using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Kondongpu.Application.Common.Interfaces;

namespace Kondongpu.Application.Features.Patients.Commands.Delete
{
    public class DeletePatientCommandValidator : AbstractValidator<DeletePatientCommand>
    {
        private readonly KondongpuDatabaseContext _context;
        public DeletePatientCommandValidator(KondongpuDatabaseContext context)
        {
            _context = context;

            RuleFor(x => x.Id).NotEmpty().WithMessage("Id ไม่สามารถเป็นค่าว่างได้").NotNull().WithMessage("โปรดระบุ Id");
        }
    }
}
